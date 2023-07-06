using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecelerationBullet : Bullet
{
    //冰冻效果的预制体
    [SerializeField] private string _flozenPrefab;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //如果碰撞的不为目标且不为障碍物则直接返回
        if (collider2D.tag != _targetTag && collider2D.tag != "Obstacles")
            return;
        //判断角色是否处于无敌时间
        if (collider2D.tag == "Player" && !collider2D.GetComponent<PlayerController>().tryDamage())
            return;
        //如果碰撞的不为障碍物则造成伤害
        if (collider2D.tag != "Obstacles")
        {
            bool isDodge = _weapon.GetComponent<Damager>().Damage(collider2D);
            //被闪避直接返回
            if (isDodge)
                return;
            Deceleration deceleration = collider2D.gameObject.GetComponentInChildren<Deceleration>();
            if (deceleration != null)
            {
                deceleration.removeFromParent();
            }
            GameObject flozen = ObjectPool.getInstance().get(_flozenPrefab);
            flozen.transform.SetParent(collider2D.gameObject.transform);
            flozen.transform.localPosition = new Vector3(0, 0, -1);
        }

        //贯穿次数-1
        _pierce--;
        if (_pierce == 0)
        {
            //生成爆炸特效
            // Instantiate(_hitVFX, transform.position, Quaternion.identity);
            GameObject _VFXObject = ObjectPool.getInstance().get(_hitVFX);
            _VFXObject.transform.position = transform.position;
            _VFXObject.GetComponent<HitVFX>()._prefabPath = _hitVFX;

            //销毁子弹
            // Destroy(gameObject);
            ObjectPool.getInstance().remove(_prefab, gameObject);
        }
    }
}

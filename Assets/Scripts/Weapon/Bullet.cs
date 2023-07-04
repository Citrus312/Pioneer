using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected string _hitVFX;
    //发射出该子弹的武器
    public GameObject _weapon;
    //子弹能够贯穿的次数
    protected int _pierce = 1;
    //预制体
    public string _prefab;
    //子弹想要击中的目标
    protected string _targetTag;

    public void setup(GameObject weapon, string prefab, string targetTag, int pierce)
    {
        _weapon = weapon;
        _prefab = prefab;
        _targetTag = targetTag;
        _pierce = pierce;
    }

    protected void Awake()
    {
        //设置子弹碰撞体参数
        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        capsuleCollider2D.isTrigger = true;

        //设置子弹刚体参数
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
        rigidbody2D.freezeRotation = true;
        // rigidbody2D.mass = 0.5f;

        //设置击中特效路径
        // _hitVFX = "Assets/Prefab/Bullet/Hit VFX.prefab";
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //如果碰撞的不为目标且不为障碍物则直接返回
        if (collider2D.tag != _targetTag && collider2D.tag != "Obstacles")
            return;
        //击退怪物
        if (collider2D.tag == "Enemy")
            collider2D.GetComponent<AIController>().OnHit(GetComponent<Rigidbody2D>().velocity.normalized);
        //判断角色是否处于无敌时间
        if (collider2D.tag == "Player" && !collider2D.GetComponent<PlayerController>().tryDamage())
            return;
        //如果碰撞的不为障碍物则造成伤害
        if (collider2D.tag != "Obstacles")
            _weapon.GetComponent<Damager>().Damage(collider2D);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject _hitVFX;
    //发射出该子弹的武器
    public GameObject _weapon;
    //子弹能够贯穿的次数
    protected int pierce = 1;
    //预制体
    public string _prefab;
    //子弹想要击中的目标
    string _targetTag;

    public void setup(GameObject weapon, string prefab, string targetTag)
    {
        _weapon = weapon;
        _prefab = prefab;
        _targetTag = targetTag;
        pierce = 1;
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
        rigidbody2D.mass = 0.05f;
    }

    protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        //如果碰撞的不为怪物则直接返回
        if (collider2D.tag != _targetTag)
            return;
        //击退怪物
        if (_targetTag == "Enemy")
            collider2D.GetComponent<AIController>().OnHit(GetComponent<Rigidbody2D>().velocity.normalized);
        //对怪物造成伤害
        _weapon.GetComponent<Damager>().Damage(collider2D);

        //贯穿次数-1
        pierce--;
        if (pierce == 0)
        {
            Instantiate(_hitVFX, transform.position, Quaternion.identity);
            //销毁子弹
            // Destroy(gameObject);
            ObjectPool.getInstance().remove(_prefab, gameObject);
        }
    }
}

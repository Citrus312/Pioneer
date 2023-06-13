/*
    怪物发射的子弹
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MonsterBullet : MonoBehaviour
{
    [SerializeField] GameObject _hitVFX;
    //发射出该子弹的武器
    public GameObject _weapon;
    //子弹能够贯穿的次数
    protected int pierce = 1;

    protected void Awake()
    {
        //设置子弹碰撞体参数
        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        capsuleCollider2D.isTrigger = true;

        //设置子弹刚体参数
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
        rigidbody2D.freezeRotation = true;
        rigidbody2D.mass = 0.5f;
    }

    protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        //如果碰撞的不为玩家则直接返回
        if (collider2D.tag != "Player")
            return;
        //对怪物造成伤害
        _weapon.GetComponent<Damager>().Damage(collider2D);

        //贯穿次数-1
        pierce--;
        if (pierce == 0)
        {
            Instantiate(_hitVFX, transform.position, Quaternion.identity);
            //销毁子弹
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
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
        rigidbody2D.mass = 0.05f;
    }

    protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        //如果碰撞的不为怪物则直接返回
        if (collider2D.tag != "Enemy")
            return;
        //对怪物造成伤害
        _weapon.GetComponent<Damager>().Damage(collider2D);

        //贯穿次数-1
        pierce--;
        if (pierce == 0)
        {
            //销毁子弹
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

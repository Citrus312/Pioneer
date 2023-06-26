/*
    近战武器的基类，主要用于碰撞判定
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class MeleeWeapon : Weapon
{
    PolygonCollider2D _polygonCollider2D;
    //判断是否正在攻击
    protected bool _isAttacking = false;
    //攻击方向
    protected Vector2 _attackDirection;

    protected new void Awake()
    {
        /*
            Weapon类的Awake
        */
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _damager = GetComponent<Damager>();
        _nextAttackTime = Time.time;

        /*
            MeleeWeapon类的Awake
        */
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _polygonCollider2D.isTrigger = true;
    }

    protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        //如果碰撞的不为怪物或没有正在攻击则直接返回
        if (collider2D.tag != "Enemy" || _isAttacking == false)
            return;
        //击退怪物
        collider2D.GetComponent<AIController>().OnHit(_attackDirection);
        //对怪物造成伤害
        _damager.Damage(collider2D);
    }
}

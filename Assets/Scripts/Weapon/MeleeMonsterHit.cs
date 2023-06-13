/*
    怪物碰撞到玩家对玩家造成伤害
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterHit : Weapon
{
    protected new void Awake()
    {
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _damager = GetComponent<Damager>();
    }

    protected void OnTriggerStay2D(Collider2D collider2D)
    {
        //如果碰撞到的是玩家且玩家不在无敌时间内则伤害玩家
        if (collider2D.tag != "Player")
            return;
        //尝试伤害玩家
        if (collider2D.GetComponent<PlayerController>().tryDamage())
        {
            _damager.Damage(collider2D);
        }
    }
}

/*
    怪物碰撞到玩家对玩家造成伤害
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterHit : Weapon
{
    //碰撞判定的圆形半径
    [SerializeField] private float radius;
    //碰撞判定的偏移
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    protected new void Awake()
    {
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _damager = GetComponent<Damager>();
    }

    // protected void OnTriggerStay2D(Collider2D collider2D)
    // {
    //     //如果碰撞到的是玩家且玩家不在无敌时间内则伤害玩家
    //     if (collider2D.tag != "Player")
    //         return;
    //     //尝试伤害玩家
    //     if (collider2D.GetComponent<PlayerController>().tryDamage())
    //     {
    //         _damager.Damage(collider2D);
    //     }
    // }

    private void Update()
    {
        //碰撞判断的偏移
        Vector2 offset = new Vector2(offsetX, offsetY);
        //进行圆形射线检测判断是否碰撞到玩家
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll((Vector2)transform.position + offset, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //判断碰撞物体是否为玩家
            if (hitColliders[i].tag.Equals("Player"))
            {
                if (hitColliders[i].GetComponent<PlayerController>().tryDamage())
                {
                    _damager.Damage(hitColliders[i]);
                }
            }
        }
    }
}

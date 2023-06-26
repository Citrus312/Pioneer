using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponAttribute))]
[RequireComponent(typeof(Damager))]
public class Weapon : MonoBehaviour
{
    //武器属性
    protected WeaponAttribute _weaponAttribute;
    //武器悬浮点
    [SerializeField] protected Transform _attachPoint;
    //武器握柄
    [SerializeField] protected Transform _grip;
    //武器尖端
    [SerializeField] protected Transform _endPoint;

    protected Damager _damager;

    //下次攻击的时间
    protected float _nextAttackTime;

    protected void Awake()
    {
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _damager = GetComponent<Damager>();
        _nextAttackTime = Time.time;
    }

    //根据需要攻击的目标tag获取攻击的方向
    protected Vector2 getAttackDirection(string tag)
    {
        //获取攻击距离
        float attackRange = _weaponAttribute.getAttackRange();

        //进行球形射线检测判断攻击范围内的敌人
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_attachPoint.position, attackRange);
        //敌人列表
        ArrayList enemy = new ArrayList();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //判断碰撞物体是否为敌人
            if (hitColliders[i].tag.Equals(tag))
            {
                enemy.Add(hitColliders[i].transform);
            }
        }

        Vector2 attackDirection = new Vector2(0, 0);
        float minDistance = 1e9f;
        for (int i = 0; i < enemy.Count; i++)
        {
            Vector2 weaponPos = _attachPoint.position, enemyPos = ((Transform)enemy[i]).position;
            //如果找到一个距离更小的敌人则更改攻击方向
            if (Vector2.Distance(weaponPos, enemyPos) < minDistance)
            {
                minDistance = Vector2.Distance(weaponPos, enemyPos);
                attackDirection = enemyPos - weaponPos;
            }
        }
        return attackDirection.normalized;
    }
}

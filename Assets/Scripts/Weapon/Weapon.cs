using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponAttribute))]
public class Weapon : MonoBehaviour
{
    //武器属性
    protected WeaponAttribute _weaponAttribute;
    //武器的transform
    protected Transform _transform;
    //武器的伤害源
    protected Damager _damager;
    //枪口
    [SerializeField] protected Transform _firePoint;

    protected void Awake()
    {
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _transform = GetComponent<Transform>();
    }

    //获取射击的方向
    protected Vector2 getShootDirection()
    {
        //获取攻击距离
        float attackRange = _weaponAttribute.getAttackRange();

        //进行球形射线检测判断攻击范围内的敌人
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_transform.position, attackRange);
        //敌人列表
        ArrayList enemy = new ArrayList();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //判断碰撞物体是否为敌人
            if (hitColliders[i].tag.Equals("Enemy"))
            {
                enemy.Add(hitColliders[i].transform);
            }
        }

        Vector2 shootDirection = new Vector2(0, 0);
        float minDistance = 1e9f;
        for (int i = 0; i < enemy.Count; i++)
        {
            Vector2 weaponPos = _transform.position, enemyPos = ((Transform)enemy[i]).position;
            //如果找到一个距离更小的敌人则更改射击方向
            if (Vector2.Distance(weaponPos, enemyPos) < minDistance)
            {
                minDistance = Vector2.Distance(weaponPos, enemyPos);
                shootDirection = enemyPos - weaponPos;
            }
        }
        return shootDirection;
    }

    // Update is called once per frame
    void Update()
    {
        //射击方向
        Vector2 shootDirection = getShootDirection();
        Debug.DrawLine(_transform.position, getShootDirection() * 100, Color.red);
        //武器需要旋转的角度
        float angle = Vector2.SignedAngle(_firePoint.position - _transform.position, shootDirection);
        //旋转武器
        transform.Rotate(new Vector3(0, 0, angle), Space.World);
    }
}

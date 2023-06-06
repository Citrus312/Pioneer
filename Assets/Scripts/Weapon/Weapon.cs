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

    protected Damager _damager;
    //枪口
    [SerializeField] protected Transform _firePoint;
    //子弹的预制体
    [SerializeField] protected GameObject _bulletPrefab;
    //下次射击的时间
    protected float _nextShootTime;

    protected void Awake()
    {
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _transform = GetComponent<Transform>();
        _nextShootTime = Time.time;
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

    //向射击方向发射一颗子弹
    protected void shoot(Vector2 shootDirection)
    {
        //实例化一颗子弹
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection, ForceMode2D.Impulse);
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

        //如果找到了射击方向则射击
        if (Time.time > _nextShootTime && shootDirection != new Vector2(0, 0))
        {
            shoot(shootDirection);
            //更新下次射击时间
            //TODO 更改考虑武器射击间隔
            _nextShootTime = Time.time + 1;
        }
    }
}

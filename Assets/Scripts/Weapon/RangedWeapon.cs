/*
    远程武器
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    //子弹的预制体
    // [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected string _bulletPrefab;
    //子弹的贯穿次数
    protected int _pierce;

    protected override void Awake()
    {
        base.Awake();
        _pierce = 1;
    }

    //高斯分布采样
    private float NextGaussian(float mean, float variance, float min, float max)
    {
        float x;
        do
        {
            x = NextGaussian(mean, variance);
        } while (x < min || x > max);
        return x;
    }

    private float NextGaussian(float mean, float standard_deviation)
    {
        return mean + NextGaussian() * standard_deviation;
    }

    private float NextGaussian()
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);
        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);
        return v1 * s;
    }

    //向射击方向发射一颗子弹
    protected void shoot(Vector2 shootDirection)
    {
        //武器的弹道数
        int bulletCount = GetComponent<WeaponAttribute>().getBulletCount();

        for (int i = 0; i < bulletCount; i++)
        {
            //实例化一颗子弹
            // GameObject bullet = Instantiate(_bulletPrefab, _endPoint.position, _endPoint.rotation);
            GameObject bullet = ObjectPool.getInstance().get(_bulletPrefab);
            bullet.transform.position = _attachPoint.position;
            // bullet.transform.rotation = _attachPoint.rotation;
            Quaternion bulletRotation = new Quaternion();
            //如果弹道数量大于1则加上一个-15°到15°的高斯分布的随机值
            float shootAngle = Vector2.SignedAngle(new Vector2(1, 0), shootDirection) + (bulletCount > 1 ? NextGaussian(0, 15.0f, -15, 15) : 0);
            bulletRotation.eulerAngles = new Vector3(0, 0, shootAngle);
            bullet.transform.rotation = bulletRotation;

            // bullet.GetComponent<Bullet>()._weapon = gameObject;
            // bullet.GetComponent<Bullet>()._prefab = _bulletPrefab;
            bullet.GetComponent<Bullet>().setup(gameObject, _bulletPrefab, "Enemy", _pierce);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(shootAngle * Mathf.Deg2Rad), Mathf.Sin(shootAngle * Mathf.Deg2Rad)), ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //射击方向
        Vector2 attackDirection = getAttackDirection("Enemy");

        //找到射击方向
        if (attackDirection != new Vector2(0, 0))
        {
            //旋转武器
            rotateWeapon(attackDirection);
            // Debug.DrawLine(_attachPoint.position, getAttackDirection("Enemy") * 100, Color.red);
            // //武器需要旋转的角度
            // float angle = Vector2.SignedAngle(_endPoint.position - _attachPoint.position, attackDirection);
            // //旋转武器
            // _attachPoint.Rotate(new Vector3(0, 0, angle), Space.World);

            //如果当前时间大于攻击冷却时间则攻击
            if (Time.time > _nextAttackTime)
            {
                shoot(attackDirection);
                //更新下次射击时间
                _nextAttackTime = Time.time + _weaponAttribute.getAttackSpeed();
            }
        }
    }
}

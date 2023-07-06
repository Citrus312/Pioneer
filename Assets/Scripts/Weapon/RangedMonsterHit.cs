/*
    怪物的远程攻击
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterHit : Weapon
{
    //子弹的预制体
    // [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected string _bulletPrefab;
    //子弹的贯穿次数
    protected int _pierce;
    //射击间隔
    private float _interval;

    protected new virtual void Awake()
    {
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _damager = GetComponent<Damager>();
        _attachPoint = transform;
        _pierce = 1;
        _interval = 3.0f;
    }

    //向射击方向发射一颗子弹
    protected void shoot(Vector2 shootDirection)
    {
        //实例化一颗子弹
        // GameObject bullet = Instantiate(_bulletPrefab, _attachPoint.position, _attachPoint.rotation);
        GameObject bullet = ObjectPool.getInstance().get(_bulletPrefab);
        bullet.transform.position = _attachPoint.position;
        // bullet.transform.rotation = _attachPoint.rotation;
        Quaternion bulletRotation = new Quaternion();
        bulletRotation.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(new Vector2(1, 0), shootDirection));
        bullet.transform.rotation = bulletRotation;

        bullet.GetComponent<Bullet>().setup(gameObject, _bulletPrefab, "Player", _pierce);
        bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //射击方向
        Vector2 attackDirection = getAttackDirection("Player");

        //找到射击方向
        if (attackDirection != new Vector2(0, 0))
        {
            Debug.DrawLine(_attachPoint.position, attackDirection * 100, Color.red);

            //如果当前时间大于攻击冷却时间则攻击
            if (Time.time > _nextAttackTime)
            {
                shoot(attackDirection);
                //更新下次射击时间
                _nextAttackTime = Time.time + _interval;
            }
        }
    }
}

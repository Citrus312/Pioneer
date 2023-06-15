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
    [SerializeField] protected string _bulletPrefab = "Assets/Prefab/Bullet/bullet.prefab";

    protected new void Awake()
    {
        /*
            Weapon的Awake
        */
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _damager = GetComponent<Damager>();
        _nextAttackTime = Time.time;

        /*
            RangedWeapon的Awake
        */
        _bulletPrefab = "Assets/Prefab/Bullet/bullet.prefab";
    }

    //向射击方向发射一颗子弹
    protected void shoot(Vector2 shootDirection)
    {
        //实例化一颗子弹
        // GameObject bullet = Instantiate(_bulletPrefab, _endPoint.position, _endPoint.rotation);
        GameObject bullet = ObjectPool.getInstance().get(_bulletPrefab);
        bullet.transform.position = _attachPoint.position;
        bullet.transform.rotation = _attachPoint.rotation;

        // bullet.GetComponent<Bullet>()._weapon = gameObject;
        // bullet.GetComponent<Bullet>()._prefab = _bulletPrefab;
        bullet.GetComponent<Bullet>().setup(gameObject, _bulletPrefab, "Enemy");
        bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //射击方向
        Vector2 attackDirection = getAttackDirection("Enemy");

        //找到射击方向
        if (attackDirection != new Vector2(0, 0))
        {
            Debug.DrawLine(_attachPoint.position, getAttackDirection("Enemy") * 100, Color.red);
            //武器需要旋转的角度
            float angle = Vector2.SignedAngle(_endPoint.position - _attachPoint.position, attackDirection);
            //旋转武器
            _attachPoint.Rotate(new Vector3(0, 0, angle), Space.World);

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

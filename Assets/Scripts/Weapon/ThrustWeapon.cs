/*
    突刺攻击类型的近战武器
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustWeapon : Weapon
{
    //判断是否正在攻击
    protected bool isAttacking = false;

    //攻击协程
    IEnumerator attack(Vector2 attackDirection)
    {
        //设置正在攻击
        isAttacking = true;

        // //当前武器尖端距离
        // float nowRange = Vector2.Distance(_attachPoint.position, _endPoint.position);

        // //刺出武器
        // while (nowRange < _weaponAttribute.getAttackRange())
        // {
        //     //计算出新的距离
        //     nowRange += attackSpeed * Time.deltaTime;
        //     //将武器刺出
        //     GetComponent<Transform>().Translate(attackDirection * attackSpeed * Time.deltaTime, Space.World);

        //     yield return null;
        // }

        // //收回武器
        // while (Vector2.Distance(_attachPoint.position, _grip.position) > 0.01f)
        // {
        //     GetComponent<Transform>().Translate((_attachPoint.position - _grip.position).normalized * attackSpeed * Time.deltaTime, Space.World);

        //     yield return null;
        // }

        /*
            固定0.5s为出刀时间，使用开始位置和目标位置做插值来显示
            开始位置和目标位置要转换到_attachPoint的相对坐标
        */
        float dur = 0.0f, time = 0.1f;
        Vector2 startPos = _attachPoint.InverseTransformPoint(GetComponent<Transform>().position);
        Vector2 endPos = _attachPoint.InverseTransformPoint(GetComponent<Transform>().position + (Vector3)attackDirection * (_weaponAttribute.getAttackRange() - Vector2.Distance(_attachPoint.position, GetComponent<Transform>().position)));
        while (dur < time)
        {
            dur += Time.deltaTime;
            GetComponent<Transform>().position = _attachPoint.TransformPoint(Vector2.Lerp(startPos, endPos, dur / time));
            yield return null;
        }

        dur = 0.0f;
        startPos = _attachPoint.InverseTransformPoint(GetComponent<Transform>().position);
        endPos = new Vector2(0, 0);
        while (dur < time)
        {
            dur += Time.deltaTime;
            GetComponent<Transform>().position = _attachPoint.TransformPoint(Vector2.Lerp(startPos, endPos, dur / time));
            yield return null;
        }

        //设置不在攻击
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //获取攻击方向
        Vector2 attackDirection = getAttackDirection();

        //如果找到了攻击方向且不处于攻击动作中
        if (attackDirection != new Vector2(0, 0) && !isAttacking)
        {
            //旋转武器
            Debug.DrawLine(_attachPoint.position, getAttackDirection() * 100, Color.red);
            //武器需要旋转的角度
            float angle = Vector2.SignedAngle(_endPoint.position - _attachPoint.position, attackDirection);
            //旋转武器
            _attachPoint.Rotate(new Vector3(0, 0, angle), Space.World);

            //如果当前时间大于下次攻击时间则开始攻击
            if (Time.time > _nextAttackTime)
            {
                //开始攻击
                StartCoroutine(attack(attackDirection));
                //更新下次攻击事件
                _nextAttackTime = Time.time + _weaponAttribute.getAttackSpeed();
            }
        }
    }
}

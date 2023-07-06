/*
    突刺攻击类型的近战武器
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustWeapon : MeleeWeapon
{
    //攻击协程
    IEnumerator attack(Vector2 attackDirection)
    {
        //设置正在攻击
        _isAttacking = true;
        GetComponent<PolygonCollider2D>().enabled = true;

        /*
            固定0.05s为出刀时间，使用开始位置和目标位置做插值来显示
            开始位置和目标位置要转换到_attachPoint的相对坐标
        */
        float dur = 0.0f, time = 0.15f;
        Vector2 startPos = _attachPoint.InverseTransformPoint(GetComponent<Transform>().position);
        Vector2 endPos = _attachPoint.InverseTransformPoint(GetComponent<Transform>().position + (Vector3)attackDirection * (_weaponAttribute.getAttackRange() * 0.02f - Vector2.Distance(_attachPoint.position, GetComponent<Transform>().position)));
        while (dur < time)
        {
            dur += Time.deltaTime;
            GetComponent<Transform>().position = _attachPoint.TransformPoint(Vector2.Lerp(startPos, endPos, dur / time));
            yield return null;
        }

        dur = 0.0f;
        startPos = _attachPoint.InverseTransformPoint(GetComponent<Transform>().position);
        endPos = _attachPoint.InverseTransformPoint(transform.position + (_attachPoint.position - _grip.position));
        while (dur < time)
        {
            dur += Time.deltaTime;
            GetComponent<Transform>().position = _attachPoint.TransformPoint(Vector2.Lerp(startPos, endPos, dur / time));
            yield return null;
        }

        //设置不在攻击
        _isAttacking = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //获取攻击方向
        Vector2 attackDirection = getAttackDirection("Enemy");

        //如果找到了攻击方向且不处于攻击动作中
        if (attackDirection != new Vector2(0, 0) && !_isAttacking)
        {
            //旋转武器
            rotateWeapon(attackDirection);
            // Debug.DrawLine(_attachPoint.position, getAttackDirection("Enemy") * 100, Color.red);
            // //武器需要旋转的角度
            // float angle = Vector2.SignedAngle(_endPoint.position - _attachPoint.position, attackDirection);
            // //旋转武器
            // _attachPoint.Rotate(new Vector3(0, 0, angle), Space.World);

            //如果当前时间大于下次攻击时间则开始攻击
            if (Time.time > _nextAttackTime)
            {
                //开始攻击
                _attackDirection = attackDirection;
                StartCoroutine(attack(attackDirection));
                //更新下次攻击事件
                _nextAttackTime = Time.time + _weaponAttribute.getAttackSpeed();
            }
        }
    }
}

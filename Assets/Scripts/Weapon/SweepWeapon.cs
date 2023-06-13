/*
    挥砍类型的武器
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepWeapon : MeleeWeapon
{
    //获取攻击的目标
    protected Transform getAttackTarget()
    {
        //获取攻击距离
        float attackRange = _weaponAttribute.getAttackRange();

        //进行球形射线检测判断攻击范围内的敌人
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_attachPoint.position, attackRange);
        //敌人列表
        List<Transform> enemy = new List<Transform>();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //判断碰撞物体是否为敌人
            if (hitColliders[i].tag.Equals("Enemy"))
            {
                enemy.Add(hitColliders[i].transform);
            }
        }

        Transform attackTarget = null;
        float minDistance = 1e9f;
        for (int i = 0; i < enemy.Count; i++)
        {
            Vector2 weaponPos = _attachPoint.position, enemyPos = ((Transform)enemy[i]).position;
            //如果找到一个距离更小的敌人则更改攻击目标
            if (Vector2.Distance(weaponPos, enemyPos) < minDistance)
            {
                minDistance = Vector2.Distance(weaponPos, enemyPos);
                attackTarget = enemy[i];
            }
        }
        return attackTarget;
    }

    //攻击协程
    IEnumerator attack(Transform attackTarget)
    {
        //设置正在攻击
        _isAttacking = true;

        //将武器伸出一段距离
        transform.Translate(attackTarget.position - _endPoint.position, Space.World);

        //目标与武器的方向
        Vector2 direction = attackTarget.position - _attachPoint.position;

        //武器旋转到指向目标的角度
        // float angle = Vector2.SignedAngle(_endPoint.position - _attachPoint.position, attackTarget.position - _attachPoint.position);
        //挥砍开始旋转的角度
        float startAngle = 90.0f;
        //挥砍动作旋转的角度
        float sweepAngle = -180.0f;
        //如果目标在武器左侧则反转挥砍
        if (direction.x < 0)
        {
            startAngle = -startAngle;
            sweepAngle = -sweepAngle;
        }

        //武器开始挥砍的eulerAngle和结束挥砍的eulerAngle
        Vector3 startEulerAngle = _attachPoint.eulerAngles + new Vector3(0, 0, startAngle);
        Vector3 endEulerAngle = startEulerAngle + new Vector3(0, 0, sweepAngle);

        /*
            固定0.1s为挥砍时间，使用开始角和目标角做插值来显示
        */
        float dur = 0.0f, time = 0.2f;
        while (dur < time)
        {
            dur += Time.deltaTime;
            _attachPoint.eulerAngles = Vector3.Lerp(startEulerAngle, endEulerAngle, dur / time);
            yield return null;
        }

        //将武器收回
        transform.Translate(_attachPoint.position - _grip.position, Space.World);

        //设置不在攻击
        _isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //获取攻击目标
        Transform attackTarget = getAttackTarget();

        //如果找到了攻击目标且不处于攻击动作中
        if (attackTarget != null && !_isAttacking)
        {
            //旋转武器
            Debug.DrawLine(_attachPoint.position, getAttackDirection("Enemy") * 100, Color.red);
            //武器需要旋转的角度
            float angle = Vector2.SignedAngle(_endPoint.position - _attachPoint.position, attackTarget.position - _attachPoint.position);
            //旋转武器
            _attachPoint.Rotate(new Vector3(0, 0, angle), Space.World);

            //如果当前时间大于下次攻击时间则开始攻击
            if (Time.time > _nextAttackTime)
            {
                //开始攻击
                StartCoroutine(attack(attackTarget));
                //更新下次攻击事件
                _nextAttackTime = Time.time + _weaponAttribute.getAttackSpeed();
            }
        }
    }
}

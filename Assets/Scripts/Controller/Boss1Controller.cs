using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(MeleeMonsterHit))]
public class Boss1Controller : AIController
{
    //是否正在释放冲刺技能
    public bool _isDashing = false;

    protected override void Awake()
    {
        base.Awake();
        //设置击退参数
        beatBackTime = 0;
        beatBackTimeSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //如果不在冲刺则移动
        if (!_isDashing)
            move(getMoveDirection());
    }
}

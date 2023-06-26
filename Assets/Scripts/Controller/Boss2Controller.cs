using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : AIController
{
    public override void OnDie()
    {
        GetComponent<Boss2Weapon>().destroyProjectiles();
    }

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
        move(getMoveDirection());
    }
}

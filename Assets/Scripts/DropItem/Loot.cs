using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : DropItem
{
    [Header("掉落参数")]
    protected int _val = 1;
    protected int _exp = 1;

    protected override void OnDie()
    {
        // 添加数据
        GameController.getInstance().updateMoney(_val);
        GameController.getInstance().addExp(_exp);
        base.OnDie();
    }
}
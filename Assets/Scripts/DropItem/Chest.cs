using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : DropItem
{
    [Header("�������")]
    protected int _val = 50;
    //protected int _exp = 1;

    protected override void OnDie()
    {
        // �������
        GameController.getInstance().updateMoney(_val);
        //GameController.getInstance().addExp(_exp);
        base.OnDie();
    }
}

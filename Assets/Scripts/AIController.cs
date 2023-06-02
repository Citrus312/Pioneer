using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    //TODO: 后期修改成向场景询问玩家位置
    //玩家位置
    public GameObject _player;

    //计算怪物移动方向
    protected Direction getMoveDirection()
    {
        //TODO: 后期修改成向场景询问玩家位置
        Vector3 playerPos = _player.transform.position;

        Vector3 aiPos = _transform.position;
        Direction moveDirection = new Direction((playerPos - aiPos).x, (playerPos - aiPos).y);
        return moveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        move(getMoveDirection());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    //获取移动方向
    protected void getMoveDirection()
    {
        float x = 0, y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 1;
        }
        move(new Direction(x, y));
    }

    // Update is called once per frame
    void Update()
    {
        getMoveDirection();
    }
}

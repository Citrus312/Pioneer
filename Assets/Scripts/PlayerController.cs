using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    // Update is called once per frame
    void Update()
    {
        //根据键盘输入进行移动
        move(new Direction(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}

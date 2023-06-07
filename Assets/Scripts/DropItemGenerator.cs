using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemGenerator : Generator
{

    void Update()
    {
        transform.position = getGeneratorPosition();//获取DropItemGenerator的位置
        generateItem(getGenerationTimer());//生成物品
    }
}

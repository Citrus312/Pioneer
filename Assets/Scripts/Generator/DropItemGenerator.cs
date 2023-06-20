using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemGenerator : Generator
{
    // 单例
    private static DropItemGenerator instance;

    // 掉落的item
    private string _droppedItemPath;

    //掉落的偏移范围
    public float _bias;

    public static DropItemGenerator getInstance()
    {
        if (instance == null)
        {
            instance = new DropItemGenerator();
        }
        return instance;
    }

    public void dropItem(Vector3 pos, int num)
    {
        for (int i = 0; i < num; i++)
        {
            generateObject(_droppedItemPath, new Vector3(pos.x + Random.Range(0, _bias), pos.y + Random.Range(0, _bias), 0));
        }
    }
}

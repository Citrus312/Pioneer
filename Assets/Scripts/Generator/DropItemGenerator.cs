using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemGenerator : Generator
{
    // 单例
    private static DropItemGenerator instance;

    // 掉落的item
    private GameObject _droppedItem;

    //一次生成掉落物的数量
    private int _minGenerateCnt = 1;
    private int _maxGenerateCnt = 3;

    // item的分数，随关卡变化
    public int _score = 1;

    public static DropItemGenerator getInstance()
    {
        if(instance == null)
        {
            instance = new DropItemGenerator();
        }
        return instance;
    }

    protected void DropItem(Vector3 pos)
    {
        int count = Random.Range(_minGenerateCnt, _maxGenerateCnt);
        GenerateObject(_droppedItem, pos, count);
    }

    protected void UpdateScore(int newScore)
    {
        _score = newScore;
    }
}

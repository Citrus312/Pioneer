using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemGenerator : Generator
{
    // 单例
    private static DropItemGenerator _instance;

    // 掉落的item列表
    public string[] _droppedLootPath;
    public string _droppedChestPath;

    //掉落的偏移范围
    public float _offset = 1.0f;

    public static DropItemGenerator getInstance()
    {
        if (_instance == null)
        {
            _instance = new DropItemGenerator();
        }
        return _instance;
    }

    public void Awake()
    {
        _droppedChestPath = "Assets/Prefab/DropItem/Chest_1.prefab";
        _instance = this;
    }

    public void dropItem(Vector3 pos, int lootCount, float dropRate)
    {
        dropLoot(pos, lootCount);

        // 是否掉落宝箱
        float randomFloat = Random.value;
        if(randomFloat < dropRate)
        {
            dropChest(pos);
        }
    }

    // 掉落货币
    void dropLoot(Vector3 pos, int num)
    {
        while(num > 0)
        {
            // 防止重叠
            pos.x += Random.Range(-1 * _offset, _offset);
            pos.y += Random.Range(-1 * _offset, _offset);

            int randomItem = Random.Range(0, _droppedLootPath.Length - 1);
            generateObject(_droppedLootPath[randomItem], pos);
        
            num--;
        }
    }

    // 掉落宝箱
    void dropChest(Vector3 pos)
    {
        generateObject(_droppedChestPath, pos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemGenerator : Generator
{
    public int minCountPerGeneration; // 每次生成的最小数量
    public int maxCountPerGeneration; // 每次生成的最大数量

    protected override void GenerateItem() // override父类的GenerateItem方法
    {
        int count = Random.Range(minCountPerGeneration, maxCountPerGeneration + 1); // 随机生成物体数量
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
            generationCount++;
        }
    }
}

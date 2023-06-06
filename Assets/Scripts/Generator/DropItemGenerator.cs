using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemGenerator : Generator
{
    public int minCountPerGeneration; // ÿ�����ɵ���С����
    public int maxCountPerGeneration; // ÿ�����ɵ��������

    protected override void GenerateItem() // override�����GenerateItem����
    {
        int count = Random.Range(minCountPerGeneration, maxCountPerGeneration + 1); // ���������������
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
            generationCount++;
        }
    }
}

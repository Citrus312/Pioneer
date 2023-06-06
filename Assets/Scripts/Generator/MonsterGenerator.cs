using UnityEngine;

public class MonsterGenerator : Generator
{
    void Start()
    {
        positionOffset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)); // �������λ��ƫ����
        transform.position += (Vector3)positionOffset; // ��λ��ƫ�����ӵ���������λ����
        base.Start(); // ���ø����Start������ʼ����ʱ��
    }

    protected override void GenerateItem() // override�����GenerateItem����
    {
        GameObject newObject = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
        generationCount++;

    }
}
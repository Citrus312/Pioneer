using UnityEngine;

public class MonsterGenerator : Generator
{
    void Start()
    {
        positionOffset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)); // 随机生成位置偏移量
        transform.position += (Vector3)positionOffset; // 将位置偏移量加到生成器的位置上
        base.Start(); // 调用父类的Start方法初始化计时器
    }

    protected override void GenerateItem() // override父类的GenerateItem方法
    {
        GameObject newObject = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
        generationCount++;

    }
}
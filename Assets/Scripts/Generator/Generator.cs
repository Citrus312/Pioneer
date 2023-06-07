using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject _player;
    public GameObject _prefabToInstantiate; // 需要生成的物体的预制体
    public float generationInterval; // 生成的时间间隔
    public int generationCountLimit; // 生成的总数限制，0表示无限制
    public Vector2 positionOffset; // 生成器的位置偏移量

    private float generationTimer; // 计时器，用于累加上次生成物体的时间
    public int generationCount; // 已生成的物体的数量

    public void Start()
    {
        transform.position += (Vector3)positionOffset; // 将位置偏移量加到生成器的位置上
        generationTimer = generationInterval; // 初始化计时器为初始时间间隔，以便在开始时立即生成一个物体
    }

    void Update()
    {
        transform.position = _player.transform.position;//每次更新将生成器位置设为玩家位置
        positionOffset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        transform.position += (Vector3)positionOffset; // 将位置偏移量加到生成器的位置上
        if (generationCountLimit > 0 && generationCount >= generationCountLimit) // 生成的数量达到了限制
        {
            return;
        }

        generationTimer += Time.deltaTime; // 计时器逐帧累加

        if (generationTimer >= generationInterval) // 计时器超过时间间隔，需要生成新的物体
        {
            GenerateItem(); // 生成物体的具体操作
            generationTimer = 0f; // 重置计时器，以便开始下一轮计时
        }
    }

    protected virtual void GenerateItem() // 生成物体的虚方法，子类可以override
    {
        GameObject newObject = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
        generationCount++;
    }
}
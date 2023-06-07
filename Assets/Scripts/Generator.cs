using UnityEngine;

public class Generator : MonoBehaviour
{
    //TODO: 后期修改成向场景询问玩家位置
    public GameObject _player;

    public GameObject _prefabToInstantiate; // 需要生成的物体的预制体
    public float generationInterval;// 生成的时间间隔
    public int generationCountLimit;// 生成的总数限制，0表示无限制
    public Vector2 positionOffset; // 生成器的位置偏移量

    private float generationTimer; // 计时器，用于累加上次生成物体的时间
    public int generationCount; // 已生成的物体的数量

    //初始化
    public void Awake()
    {
        generationInterval = 1f;
        generationCountLimit = 5;
        generationTimer = generationInterval; // 初始化计时器为初始时间间隔，以便在开始时立即生成一个物体
    }

    protected void calculateGenerationTimer()
    {
        generationTimer += Time.deltaTime;
    }
    protected float getGenerationTimer()
    {
        return generationTimer;
    }

    protected bool isFinishGeneration()
    {
        if (generationCountLimit > 0 && generationCount >= generationCountLimit) // 生成的数量达到了限制
        {
            return true;
        }
        else
            return false;
    }
    // 生成物体的虚方法，子类可以override
    protected virtual void generateItem(float generationTimer)
    {
        print(generationCountLimit + " " + generationCount);
        if (generationCountLimit > 0 && generationCount >= generationCountLimit) // 生成的数量达到了限制
        {
            return;
        }

        getGenerationTimer();
        print(generationTimer + " " + generationInterval);
        if (generationTimer >= generationInterval) // 计时器超过时间间隔，需要生成新的物体
        {
            print("enter if");
            GameObject newObject = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
            generationCount++;
            generationTimer = 0f; // 重置计时器，以便开始下一轮计时
        }
    }

    //获取生成器的位置
    protected virtual Vector3 getGeneratorPosition()
    {
        transform.position = _player.transform.position;//每次更新将生成器位置设为玩家位置
        positionOffset = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)); // 随机生成位置偏移量
        transform.position += (Vector3)positionOffset; // 将位置偏移量加到生成器的位置上
        return transform.position;
    }
}
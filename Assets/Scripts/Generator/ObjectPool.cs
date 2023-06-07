using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 单例
    public static ObjectPool poolInstance;

    // 池子要存储的物体
    public GameObject Object;
    // 内存区（队列）
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    // 池子的初始容量
    public int defaultCount;
    // 池子的最大容量
    public int maxCount;

    private void Awake()
    {
        poolInstance = this;
        defaultCount = 10;
        maxCount = 20;
    }

    // 对池子进行初始化（创建初始容量个数的物体）
    public void Init()
    {
        GameObject obj;
        for (int i = 0; i < defaultCount; i++)
        {
            obj = Instantiate(Object, this.transform);
            // 将生成的对象入队
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }
    // 从池子中取出物体
    public GameObject Get()
    {
        GameObject tmp;
        // 如果池子内有物体，从池子取出一个物体
        if (objectPool.Count > 0)
        {
            // 将对象出队
            tmp = objectPool.Dequeue();
            tmp.SetActive(true);
        }
        // 如果池子中没有物体，直接新建一个物体
        else
        {
            tmp = Instantiate(Object, this.transform);
        }
        return tmp;
    }
    // 将物体回收进池子
    public void Remove(GameObject obj)
    {
        // 池子中的物体数目不超过最大容量
        if (objectPool.Count <= maxCount)
        {
        	// 该对象没有在队列中
            if (!objectPool.Contains(obj))
            {
                // 将对象入队
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        // 超过最大容量就销毁
        else
        {
            Destroy(obj);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 单例
    public static ObjectPool _poolInstance;
    // 内存区（队列）
    protected Queue<GameObject>[] _objectPool;

    public GameObject[] _objectList;

    public static ObjectPool getInstance()
    {
        if (_poolInstance == null)
        {
            _poolInstance = new ObjectPool();
        }
        return _poolInstance;
    }

    private void Awake()
    {
        _objectPool = new Queue<GameObject>[_objectList.Length];
    }
    // 从池子中取出物体
    public GameObject get(int idx)
    {
        if (idx < 0 || idx >= _objectList.Length) return null;

        GameObject tmp;
        // 如果池子内有物体，从池子取出一个物体
        if (_objectPool[idx].Count > 0)
        {
            // 将对象出队
            tmp = _objectPool[idx].Dequeue();
            tmp.SetActive(true);
        }
        // 如果池子中没有物体，直接新建一个物体
        else
        {
            tmp = Instantiate(_objectList[idx], this.transform);
        }
        return tmp;
    }
    // 将物体回收进池子
    public void remove(GameObject obj)
    {
        //int idx = obj.GetComponent<CharacterAttribute>().getPoolIdx();
        if (idx < 0 || idx >= _objectList.Length) return;

        // 该对象没有在队列中
        if (!_objectPool[idx].Contains(obj))
        {
            // 将对象入队
            _objectPool[idx].Enqueue(obj);
            obj.SetActive(false);
        }
    }
}

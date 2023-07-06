using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectPool : MonoBehaviour
{
    // 单例
    public static ObjectPool _poolInstance;
    // 内存区（队列）
    protected Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();
    //实例化的所有对象
    protected Dictionary<string, List<GameObject>> _objectList = new Dictionary<string, List<GameObject>>();

    public static ObjectPool getInstance()
    {
        // if (_poolInstance == null)
        // {
        //     _poolInstance = ObjectFactory.CreateInstance<ObjectPool>();
        // }
        return _poolInstance;
    }

    private void Awake()
    {
        _poolInstance = this;
    }

    // 从池子中取出物体
    public GameObject get(string prefabPath)
    {
        GameObject tmp;

        //Debug.Log("get " + UnityEditor.AssetDatabase.GetAssetPath(objPrefab));

        // 如果有该对象的池
        if (_objectPool.ContainsKey(prefabPath))
        {
            // 如果池子内有物体，从池子取出一个物体
            if (_objectPool[prefabPath].Count > 0)
            {
                // 将对象出队
                tmp = _objectPool[prefabPath].Dequeue();
                tmp.SetActive(true);
                // Debug.Log("从池子拿 " + tmp);
            }
            // 如果池子中没有物体，直接新建一个物体
            else
            {
                GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
                tmp = Instantiate(prefabObject, this.transform);
                //保存进objectList中
                _objectList[prefabPath].Add(tmp);
                // Debug.Log("有池子，没物体 " + tmp);
            }
        }
        else
        {
            // 新建一个池
            GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
            tmp = Instantiate(prefabObject, this.transform);
            //Debug.Log("tmp " + UnityEditor.AssetDatabase.GetAssetPath(tmp));
            Queue<GameObject> q = new Queue<GameObject>();
            _objectPool.Add(prefabPath, q);
            List<GameObject> l = new List<GameObject>();
            l.Add(tmp);
            _objectList.Add(prefabPath, l);
            // Debug.Log("新建池 " + prefabPath);
        }

        return tmp;
    }
    // 将物体回收进池子
    public void remove(string prefabPath, GameObject obj)
    {
        // Debug.Log("path:" + prefabPath);
        // 若存在池子
        if (_objectPool.ContainsKey(prefabPath))
        {
            if (_objectPool[prefabPath].Contains(obj))
                return;
            // 入队
            _objectPool[prefabPath].Enqueue(obj);
            obj.SetActive(false);
            // Debug.Log("remove " + prefabPath);
        }
    }

    //回收生成的所有物体
    public void removeAll()
    {
        foreach (string prefabPath in _objectList.Keys)
        {
            foreach (GameObject obj in _objectList[prefabPath])
            {
                remove(prefabPath, obj);
            }
        }
    }
}

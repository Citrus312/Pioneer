using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectPool : MonoBehaviour
{
    // 单例
    private static ObjectPool _poolInstance;
    // 内存区（队列）
    protected Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();

    public static ObjectPool getInstance()
    {
        if(_poolInstance == null)
        {
            _poolInstance = ObjectFactory.CreateInstance<ObjectPool>();
        }
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
        if(_objectPool.ContainsKey(prefabPath))
        {
            // 如果池子内有物体，从池子取出一个物体
            if (_objectPool[prefabPath].Count > 0)
            {
                // 将对象出队
                tmp = _objectPool[prefabPath].Dequeue();
                tmp.SetActive(true);
                Debug.Log("从池子拿 " + tmp);
            }
            // 如果池子中没有物体，直接新建一个物体
            else
            {
                GameObject prefabObject = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                tmp = Instantiate(prefabObject, this.transform);
                Debug.Log("有池子，没物体 " + tmp);
            }
        }
        else
        {
            // 新建一个池
            GameObject prefabObject = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            tmp = Instantiate(prefabObject, this.transform);
            //Debug.Log("tmp " + UnityEditor.AssetDatabase.GetAssetPath(tmp));
            Queue<GameObject> q = new Queue<GameObject>();
            _objectPool.Add(prefabPath, q);
            Debug.Log("新建池");
        }

        return tmp;
    }
    // 将物体回收进池子
    public void remove(string prefabPath, GameObject obj)
    {
        Debug.Log("path:" + prefabPath);
        // 若存在池子
        if(_objectPool.ContainsKey(prefabPath))
        {
            // 入队
            _objectPool[prefabPath].Enqueue(obj);
            obj.SetActive(false);
        }

    }
}

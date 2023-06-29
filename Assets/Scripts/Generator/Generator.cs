﻿using UnityEngine;

public class Generator : MonoBehaviour
{
    // 生成object
    protected virtual GameObject generateObject(string prefabPath, Vector3 pos)
    {
        GameObject newObject = ObjectPool.getInstance().get(prefabPath);
        newObject.transform.position = pos;
        Damageable dam = newObject.GetComponent<Damageable>();
        if (dam != null)
        {
            dam._prefabPath = prefabPath;
        }
        return newObject;
    }
}
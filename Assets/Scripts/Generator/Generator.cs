using UnityEngine;

public class Generator : MonoBehaviour
{
    // 生成object
    protected virtual void generateObject(string prefabPath, Vector3 pos)
    {
        GameObject newObject = ObjectPool.getInstance().get(prefabPath);
        newObject.transform.position = pos;
        newObject.GetComponent<Damageable>()._prefabPath = prefabPath;
    }
}
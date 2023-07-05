using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraEffect : MonoBehaviour
{
    // 燃烧时长
    protected float _duration;
    // 预制体路径
    public string _prefabPath;
    // 父角色
    protected GameObject _character;

    private void OnEnable()
    {
        StartCoroutine(effect());
    }

    protected virtual IEnumerator effect()
    {
        yield return null;
    }

    public virtual void removeFromParent()
    {
        // 将对象从父物体中移除
        transform.SetParent(null);

        // 回收
        ObjectPool.getInstance().remove(_prefabPath, gameObject);
    }
}

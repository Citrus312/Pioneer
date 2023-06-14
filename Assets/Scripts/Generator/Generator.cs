using UnityEngine;

public class Generator : MonoBehaviour
{
    protected int _cntLimit; // 最大数量

    protected int _cnt; // 当前数量

    // 多个生成时的偏移
    protected float _posOffset;

    public virtual void Start()
    {
        _cntLimit = 25;
        _cnt = 0;
        _posOffset = 1.0f;
        // 读取数据等等(待填写)
    }

    // 生成object
    protected virtual void generateObject(string prefabPath, Vector3 pos, int num)
    {
        // 若数量未超过限制
        if(_cnt <= _cntLimit)
        {
            if(num > 1){
                // 多个生成
                pos = new Vector3(pos.x + Random.Range(-1 * _posOffset, _posOffset), pos.y + Random.Range(-1 * _posOffset, _posOffset), pos.z);
                generateObject(prefabPath, pos, num - 1);
            }
            // 从对象池获取
            GameObject newObject = ObjectPool.getInstance().get(prefabPath);
            
            newObject.transform.position = pos;
            newObject.SetActive(true);
            _cnt++;
        }
    }
}
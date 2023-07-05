using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShadow : MonoBehaviour
{
    // 空物体作为倒影载体
    public string _prefabPath = "Assets/Prefab/Obstacles/Lake/EmptyObject.prefab";
    // 屏幕外倒影
    private GameObject copyObj;
    // 倒影sprite
    private SpriteRenderer copySprite;
    // 原物体sprite
    private SpriteRenderer selfSprite;
    void OnEnable()
    {
        // 生成倒影
        copyObj = ObjectPool.getInstance().get(_prefabPath);
        copyObj.name = this.name + "_shadow";
        copySprite = copyObj.GetComponent<SpriteRenderer>();
        selfSprite = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 动态更新倒影
        copySprite.sprite = selfSprite.sprite;
        copySprite.flipX = selfSprite.flipX;
        copyObj.transform.position = new Vector3(this.transform.position.x + 60.0f, this.transform.position.y);
        copyObj.transform.rotation = Quaternion.Inverse(this.transform.rotation);
        copyObj.transform.localScale = new Vector3(this.transform.lossyScale.x, -this.transform.lossyScale.y);
    }

    public void removeWaterShadow()
    {
        // 移除
        ObjectPool.getInstance().remove(_prefabPath, copyObj);
    }
}

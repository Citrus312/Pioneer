using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShadow : MonoBehaviour
{
    public string _prefabPath = "Assets/Prefab/Obstacles/Lake/EmptyObject.prefab";
    private GameObject copyObj;
    private SpriteRenderer copySprite;
    private SpriteRenderer selfSprite;
    void Start()
    {
        copyObj = ObjectPool.getInstance().get(_prefabPath);
        copyObj.name = this.name + "_shadow";
        copySprite = copyObj.GetComponent<SpriteRenderer>();
        selfSprite = this.GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        copySprite.sprite = selfSprite.sprite;
        copySprite.flipX = !selfSprite.flipX;
        copyObj.transform.position = new Vector3(-this.transform.position.x + 60.0f, this.transform.position.y);
        copyObj.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y);
    }

    public void removeWaterShadow()
    {
        ObjectPool.getInstance().remove(_prefabPath, copyObj);
    }
}

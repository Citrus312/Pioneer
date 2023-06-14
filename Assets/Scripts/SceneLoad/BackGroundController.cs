using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    [SerializeField] private float parallaxCoefficient;
    private Sprite bgSprite;
    private Texture2D bgTexture;
    private float textureUnitSizeX;

    // Start is called before the first frame update
    void Start()
    {
        parallaxCoefficient = 0.5f;
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        bgSprite = GetComponent<SpriteRenderer>().sprite;
        bgTexture = bgSprite.texture;
        textureUnitSizeX = bgTexture.width / bgSprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxCoefficient,0,0);
        lastCameraPosition = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
           
            float offsetPositionX = (cameraTransform.position.x- transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x+offsetPositionX, transform.position.y, transform.position.z);
        }
    }
}


/*
视差滚动：
通常相机和人物绑定以一定的速度向前，而背后的景物设置不同的速度这样就实现了视差效果
1.获取主相机的transform，记录相机的初始位置
2.记录相机的移动的位置，刷新背景的位置(因为保证不同速度所以要乘以一个系数),刷新相机初始位置(通过计算每帧的路程实现速度不一样)

无限背景：
1.获取sprite，再获取texture
2.计算材质再unity中占几个单位(unity中的默认单位是:1单位100px)
3.当摄像机位置减去背景的位置的绝对值（左右）大于材质宽度（unity单位）时刷新背景位置
*/

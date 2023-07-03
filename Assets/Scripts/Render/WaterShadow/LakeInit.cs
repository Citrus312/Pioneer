using UnityEngine;

public class LakeInit : MonoBehaviour
{
    public int width = 1920; // 渲染纹理的宽度
    public int height = 1080; // 渲染纹理的高度
    public RenderTextureFormat format = RenderTextureFormat.ARGB32; // 渲染纹理的格式
    public LayerMask layerToRender; // 指定要渲染的图层
    private RenderTexture renderTexture;

    private Camera tempCamera;
    
    void Start()
    {
        // 创建一个新的相机
        GameObject cameraObject = new GameObject("NewCamera");
        cameraObject.transform.position = new Vector3(this.transform.position.x + 60.0f, this.transform.position.y + 4.5f, -10.0f);
        cameraObject.transform.rotation = this.transform.rotation;
        
        // 添加相机组件到相机对象上
        tempCamera = cameraObject.AddComponent<Camera>();
        tempCamera.orthographic = true;
        tempCamera.orthographicSize = 2.1f;
        tempCamera.cullingMask = layerToRender;

        // 生成渲染纹理
        renderTexture = RenderTexture.GetTemporary(width, height, 0, format);

        // 设置纹理的相关属性，比如滤波模式、是否可读等
        renderTexture.filterMode = FilterMode.Point;
        
        // 设置相机的输出纹理属性
        tempCamera.targetTexture = renderTexture;

        // 设置材质的纹理属性
        GetComponent<SpriteRenderer>().material.SetTexture("_CameraOutput", renderTexture);
    }
}

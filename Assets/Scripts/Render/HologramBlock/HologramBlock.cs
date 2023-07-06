using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public sealed class HologramBlock : VolumeComponent, IPostProcessComponent
{
    [Tooltip("是否开启效果")]
    public BoolParameter enableEffect = new BoolParameter(true);

    [Range(0f,1f),Tooltip("全息投影的扫描线抖动")]
    public ClampedFloatParameter scanLineJitter = new ClampedFloatParameter(0.1f, 0.0f, 1.0f);

    [Range(0f, 1f), Tooltip("全息投影的扫描线颜色抖动")]
    public ClampedFloatParameter colorDrift = new ClampedFloatParameter(0.1f, 0.0f, 1.0f);

    [Range(0f, 100f), Tooltip("全息投影的错误视频速度")]
    public ClampedFloatParameter speed = new ClampedFloatParameter(0.1f, 0.0f, 100.0f);

    [Range(0f, 100f), Tooltip("全息投影的错误视频方块大小")]
    public ClampedFloatParameter blockSize = new ClampedFloatParameter(0.1f, 0.0f, 100.0f);

    public bool IsActive() => enableEffect==true;

    public bool IsTileCompatible() => false;
}
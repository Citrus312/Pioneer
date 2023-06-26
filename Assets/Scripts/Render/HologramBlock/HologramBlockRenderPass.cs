using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HologramBlockRenderPass : ScriptableRenderPass
{
    static readonly string k_RenderTag = "Hologram Effects";
    static readonly int _MainTexId = Shader.PropertyToID("_MainTex");
    static readonly int _BlockSizeId = Shader.PropertyToID("_BlockSize");
    static readonly int _SpeedId = Shader.PropertyToID("_Speed");
    static readonly int _Params = Shader.PropertyToID("_Params");
    public int blitShaderPassIndex = 0;
    private RenderTargetHandle destination { get; set; }
    Material holoMat;
    HologramBlock hologramBlock;
    RenderTargetIdentifier currentTarget;
    RenderTargetHandle m_temporaryColorTexture;
    public FilterMode filterMode { get; set; }
    public HologramBlockRenderPass()
    {
        var shader = Shader.Find("Hidden/HL/HologramBlockPE");
        holoMat = CoreUtils.CreateEngineMaterial(shader);
        m_temporaryColorTexture.Init("temporaryColorTexture");

    }
    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {

    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (holoMat == null)
        {
            UnityEngine.Debug.LogError("材质没找到！"); 
            return;
        }
        if (!renderingData.cameraData.postProcessEnabled) return;
        //通过队列来找到HologramBlock组件，然后
        var stack = VolumeManager.instance.stack;
        hologramBlock = stack.GetComponent<HologramBlock>();
        if (hologramBlock == null){return;}
        if (!hologramBlock.IsActive())return;

        var cmd = CommandBufferPool.Get(k_RenderTag);
        //UnityEngine.Debug.LogError("Execute渲染中！");
        Render(cmd, ref renderingData);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);        
    }

    void Render(CommandBuffer cmd,ref RenderingData renderingData)
    {
        if (renderingData.cameraData.isSceneViewCamera) return;
        var source = currentTarget;

        holoMat.SetFloat(_BlockSizeId, hologramBlock.blockSize.value);
        holoMat.SetFloat(_SpeedId, hologramBlock.speed.value);
        var sl_thresh = Mathf.Clamp01(1.0f - hologramBlock.scanLineJitter.value * 1.2f);
        var sl_disp = 0.002f + Mathf.Pow(hologramBlock.scanLineJitter.value, 3) * 0.05f;
        var cd = new Vector2(hologramBlock.colorDrift.value * 0.04f, Time.time * 606.11f);

        holoMat.SetVector(_Params, new Vector4( sl_disp, sl_thresh, cd.x, cd.y));

        RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
        opaqueDesc.depthBufferBits = 0;
        //不能读写同一个颜色target，创建一个临时的render Target去blit
        if (destination == RenderTargetHandle.CameraTarget)
        {
            cmd.GetTemporaryRT(m_temporaryColorTexture.id, opaqueDesc, filterMode) ;
            Blit(cmd, source, m_temporaryColorTexture.Identifier(), holoMat, blitShaderPassIndex);
            Blit(cmd, m_temporaryColorTexture.Identifier(), source);
        }
        else
        {
            Blit(cmd, source, destination.Identifier(), holoMat, blitShaderPassIndex);
            //cmd.Blit(source, destination.Identifier(), holoMat, shaderPass);
        }

    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
        if (destination == RenderTargetHandle.CameraTarget)
            cmd.ReleaseTemporaryRT(m_temporaryColorTexture.id);
    }

    public void Setup(in RenderTargetIdentifier currentTarget, RenderTargetHandle dest)
    {
        this.destination = dest;
        this.currentTarget = currentTarget;
    }
}

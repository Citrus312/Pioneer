using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HologramBlockRenderFeature : ScriptableRendererFeature
{
    HologramBlockRenderPass m_ScriptablePass;

    public override void Create()
    {
        m_ScriptablePass = new HologramBlockRenderPass();
        m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        m_renderTargetHandle.Init("_ScreenTexture2");
    }
    RenderTargetHandle m_renderTargetHandle;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var dest =  RenderTargetHandle.CameraTarget;
        m_ScriptablePass.Setup(renderer.cameraColorTarget, dest);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


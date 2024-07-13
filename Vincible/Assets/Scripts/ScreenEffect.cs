using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenEffect : ScriptableRendererFeature
{
	class CustomRenderPass : ScriptableRenderPass
	{
		public Material material;
		private RenderTargetIdentifier source { get; set; }
		private RenderTargetHandle temporaryColorTexture;

		public CustomRenderPass(Material material)
		{
			this.material = material;
			temporaryColorTexture.Init("_TemporaryColorTexture");
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			source = renderingData.cameraData.renderer.cameraColorTarget;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = CommandBufferPool.Get("CustomPostProcessing");

			RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
			opaqueDesc.depthBufferBits = 0;
			cmd.GetTemporaryRT(temporaryColorTexture.id, opaqueDesc, FilterMode.Bilinear);

			Blit(cmd, source, temporaryColorTexture.Identifier(), material, 0);
			Blit(cmd, temporaryColorTexture.Identifier(), source);

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.ReleaseTemporaryRT(temporaryColorTexture.id);
		}
	}

	public Material material;
	CustomRenderPass m_ScriptablePass;

	public override void Create()
	{
		m_ScriptablePass = new CustomRenderPass(material)
		{
			renderPassEvent = RenderPassEvent.AfterRenderingTransparents
		};
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		renderer.EnqueuePass(m_ScriptablePass);
	}
}
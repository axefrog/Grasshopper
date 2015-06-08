using System;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Blending;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Graphics.Rendering.Rasterization;

namespace Grasshopper.Graphics
{
	/// <summary>
	/// This is the grand overlord of rendering with a single graphics device. Multiple windows, renderers
	/// and other device-dependent resources are all directly or indirectly managed by this class. Multiple
	/// graphics contexts can exist, but they cannot share resources between each other.
	/// </summary>
	public interface IGraphicsContext : IDisposable
	{
		void Initialize();
		ITextureResourceManager TextureResourceManager { get; }
		ITextureSamplerManager TextureSamplerManager { get; }
		IMaterialManager MaterialManager { get; }
		IMeshGroupBufferManager MeshGroupBufferManager { get; }
		IBlendStateManager BlendStateManager { get; }
		IRasterizerStateManager RasterizerStateManager { get; }

		IRenderTargetFactory RenderTargetFactory { get; }
		IMeshInstanceBufferManagerFactory MeshInstanceBufferManagerFactory { get; }
		IConstantBufferManagerFactory ConstantBufferManagerFactory { get; }
	}
}
using System;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Blending;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Graphics.Rendering.Rasterization;
using Grasshopper.Input;
using Grasshopper.Platform;
using Grasshopper.SharpDX.Graphics.Materials;
using Grasshopper.SharpDX.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	class GraphicsContext : IGraphicsContext
	{
		public GraphicsContext(Lazy<IFileStore> files, IInputContext input, bool enableDebugMode = false)
		{
			DeviceManager = new DeviceManager(enableDebugMode);
			
			TextureResourceManager = new TextureResourceManager(DeviceManager, files);
			TextureSamplerManager = new TextureSamplerManager(DeviceManager);
			MaterialManager = new MaterialManager(this);
			MeshGroupBufferManager = new MeshGroupBufferManager(DeviceManager);
			BlendStateManager = new BlendStateManager(DeviceManager);
			RasterizerStateManager = new RasterizerStateManager(DeviceManager);

			RenderTargetFactory = new RenderTargetFactory(this, input);
			MeshInstanceBufferManagerFactory = new MeshInstanceBufferManagerFactory(DeviceManager);
			ConstantBufferManagerFactory = new ConstantBufferManagerFactory(DeviceManager);
		}

		public DeviceManager DeviceManager { get; private set; }

		public ITextureResourceManager TextureResourceManager { get; private set; }
		public ITextureSamplerManager TextureSamplerManager { get; private set; }
		public IMaterialManager MaterialManager { get; private set; }
		public IMeshGroupBufferManager MeshGroupBufferManager { get; private set; }
		public IBlendStateManager BlendStateManager { get; private set; }
		public IRasterizerStateManager RasterizerStateManager { get; private set; }

		public IRenderTargetFactory RenderTargetFactory { get; private set; }
		public IMeshInstanceBufferManagerFactory MeshInstanceBufferManagerFactory { get; private set; }
		public IConstantBufferManagerFactory ConstantBufferManagerFactory { get; private set; }

		public void Initialize()
		{
			DeviceManager.Initialize();
			
			if(!TextureSamplerManager.Exists("default"))
				TextureSamplerManager.Create("default", TextureSamplerSettings.Default()).Activate(0);
			if(!BlendStateManager.Exists("none"))
				BlendStateManager.Create("none", BlendSettings.None()).Activate();
			if(!BlendStateManager.Exists("default"))
				BlendStateManager.Create("default", BlendSettings.DefaultEnabled());
			if(!RasterizerStateManager.Exists("default"))
				RasterizerStateManager.Create("default", RasterizerSettings.Default()).Activate();
		}

		private bool _disposed;
		public void Dispose()
		{
			if(_disposed) return;
			DeviceManager.Dispose();
			MaterialManager.Dispose();
			MeshGroupBufferManager.Dispose();
			TextureResourceManager.Dispose();
			TextureSamplerManager.Dispose();
			BlendStateManager.Dispose();
			RasterizerStateManager.Dispose();
			_disposed = true;
		}
	}
}

using System;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;
using Grasshopper.Platform;
using Grasshopper.SharpDX.Graphics.Materials;
using Grasshopper.SharpDX.Graphics.Rendering;
using Grasshopper.SharpDX.Input;

namespace Grasshopper.SharpDX.Graphics
{
	class GraphicsContext : IGraphicsContext
	{
		public GraphicsContext(Lazy<IFileStore> files, IInputContext input, bool enableDebugMode = false)
		{
			DeviceManager = new DeviceManager(enableDebugMode);
			RenderHostFactory = new RenderHostFactory(this, input);
			TextureResourceManager = new TextureResourceManager(DeviceManager, files);
			TextureSamplerManager = new TextureSamplerManager(DeviceManager);
			MaterialManager = new MaterialManager(this);
			MeshGroupBufferManager = new MeshGroupBufferManager(DeviceManager);
			MeshInstanceBufferManagerFactory = new MeshInstanceBufferManagerFactory(DeviceManager);
			ConstantBufferManagerFactory = new ConstantBufferManagerFactory(DeviceManager);
		}

		public DeviceManager DeviceManager { get; private set; }
		public IRenderHostFactory RenderHostFactory { get; private set; }
		public ITextureResourceManager TextureResourceManager { get; private set; }
		public ITextureSamplerManager TextureSamplerManager { get; private set; }
		public IMaterialManager MaterialManager { get; private set; }
		public IMeshGroupBufferManager MeshGroupBufferManager { get; private set; }
		public IMeshInstanceBufferManagerFactory MeshInstanceBufferManagerFactory { get; private set; }
		public IConstantBufferManagerFactory ConstantBufferManagerFactory { get; private set; }

		public void Initialize()
		{
			DeviceManager.Initialize();
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
			_disposed = true;
		}
	}
}

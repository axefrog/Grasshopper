using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Platform;
using Grasshopper.SharpDX.Graphics.Materials;
using Grasshopper.SharpDX.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class GraphicsContext : IGraphicsContext
	{
		public GraphicsContext(IFileStore files, bool enableDebugMode = false)
		{
			DeviceManager = new DeviceManager(enableDebugMode);
			RenderHostFactory = new RenderHostFactory(this);
			TextureResourceManager = new TextureResourceManager(DeviceManager, files);
			TextureSamplerManager = new TextureSamplerManager(DeviceManager);
			MaterialManager = new MaterialManager(DeviceManager);
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

using Grasshopper.Assets;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class GraphicsContext : IGraphicsContext
	{
		public GraphicsContext(IAssetResourceFactory assets)
		{
			DeviceManager = new DeviceManager();
			TextureLoader = new TextureLoader(DeviceManager, assets);
			RendererFactory = new RendererFactory(this);
		}

		public DeviceManager DeviceManager { get; private set; }
		public IRendererFactory RendererFactory { get; private set; }
		public ITextureLoader TextureLoader { get; private set; }

		public void Initialize()
		{
			DeviceManager.Initialize();
		}

		private bool _disposed;
		public void Dispose()
		{
			if(_disposed) return;
			DeviceManager.Dispose();
			_disposed = true;
		}
	}
}

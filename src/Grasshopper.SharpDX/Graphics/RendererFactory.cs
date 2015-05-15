using Grasshopper.Graphics;

namespace Grasshopper.SharpDX.Graphics
{
	public class RendererFactory : IRendererFactory
	{
		private readonly GraphicsContext _graphicsContext;

		public RendererFactory(GraphicsContext graphicsContext)
		{
			_graphicsContext = graphicsContext;
		}

		public IWindowRenderer CreateWindowed()
		{
			var window = new AppWindow();
			var viewportManager = new ViewportManager(window, _graphicsContext.DeviceManager);
			var renderer = new WindowRenderer(_graphicsContext, viewportManager);
			
			viewportManager.Initialize();
			renderer.Initialize();
		
			return renderer;
		}
	}
}

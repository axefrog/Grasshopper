using Grasshopper.Graphics.Rendering;

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
			var rendererContext = new WindowRendererContext(_graphicsContext, viewportManager);
			var renderer = new WindowRenderer(_graphicsContext, viewportManager, rendererContext);
			
			viewportManager.Initialize();
			renderer.Initialize();
		
			return renderer;
		}
	}
}

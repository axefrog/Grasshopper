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
			var rendererContext = new WindowRendererContext(_graphicsContext.DeviceManager, window);
			rendererContext.Initialize();
			var renderer = new WindowRenderer(window, rendererContext);
			return renderer;
		}
	}
}

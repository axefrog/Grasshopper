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
			var renderer = new WindowRenderer(_graphicsContext.DeviceManager);
			renderer.Initialize();
			return renderer;
		}
	}
}

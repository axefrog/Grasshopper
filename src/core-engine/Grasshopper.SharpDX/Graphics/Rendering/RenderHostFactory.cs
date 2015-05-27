using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class RenderHostFactory : IRenderHostFactory
	{
		private readonly GraphicsContext _graphicsContext;

		public RenderHostFactory(GraphicsContext graphicsContext)
		{
			_graphicsContext = graphicsContext;
		}

		public IWindowRenderHost CreateWindowed()
		{
			var renderer = new WindowRenderHost(_graphicsContext.DeviceManager);
			renderer.Initialize();
			return renderer;
		}
	}
}

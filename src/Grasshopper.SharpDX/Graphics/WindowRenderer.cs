using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class WindowRenderer : Renderer<IWindowRendererContext>, IWindowRenderer
	{
		public WindowRenderer(GraphicsContext graphicsContext, ViewportManager viewportManager, WindowRendererContext rendererContext)
			: base(graphicsContext, viewportManager, rendererContext)
		{
		}

		public IAppWindow Window { get { return ViewportManager.Window; } }

		public override bool Render(RenderFrameHandler<IWindowRendererContext> run)
		{
			if(Window != null && !Window.NextFrame())
				return false;

			return base.Render(run);
		}
	}
}
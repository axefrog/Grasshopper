using Grasshopper.Graphics;

namespace Grasshopper.SharpDX.Graphics
{
	public class WindowRenderer : Renderer, IWindowRenderer
	{
		public WindowRenderer(GraphicsContext graphicsContext, ViewportManager viewportManager) : base(graphicsContext, viewportManager)
		{
		}

		public IAppWindow Window { get { return ViewportManager.Window; } }

		public override bool Render(RenderFrameHandler run)
		{
			if(Window != null && !Window.NextFrame())
				return false;

			return base.Render(run);
		}
	}
}
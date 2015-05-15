using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class WindowRenderer : Renderer<IWindowRendererContext>, IWindowRenderer
	{
		private readonly IAppWindow _window;

		public WindowRenderer(IAppWindow window, IWindowRendererContext rendererContext) : base(rendererContext)
		{
			_window = window;
		}

		public IAppWindow Window { get { return _window; } }

		public override bool Render(RenderFrameHandler<IWindowRendererContext> run)
		{
			if(Window != null && !Window.NextFrame())
				return false;

			return base.Render(run);
		}
	}
}
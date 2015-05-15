using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class WindowRenderer : Renderer<IWindowRendererContext>, IWindowRenderer
	{
		private readonly WindowRendererContext _rendererContext;

		public WindowRenderer(DeviceManager deviceManager) : this(new WindowRendererContext(deviceManager))
		{
		}

		private WindowRenderer(WindowRendererContext rendererContext) : base(rendererContext)
		{
			_rendererContext = rendererContext;
		}

		public IAppWindow Window { get { return _rendererContext.Window; } }

		public override bool Render(RenderFrameHandler<IWindowRendererContext> run)
		{
			if(Window != null && !Window.NextFrame())
				return false;

			return base.Render(run);
		}

		public void Initialize()
		{
			_rendererContext.Initialize();
		}
	}
}
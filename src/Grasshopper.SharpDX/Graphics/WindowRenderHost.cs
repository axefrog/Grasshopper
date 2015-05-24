using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class WindowRenderHost : RenderHost<IWindowRenderContext>, IWindowRenderHost
	{
		private readonly WindowRenderContext _renderContext;

		public WindowRenderHost(DeviceManager deviceManager) : this(new WindowRenderContext(deviceManager))
		{
		}

		private WindowRenderHost(WindowRenderContext renderContext) : base(renderContext)
		{
			_renderContext = renderContext;
		}

		public IAppWindow Window { get { return _renderContext.Window; } }

		public override void Render(RenderFrameHandler<IWindowRenderContext> run)
		{
			if(Window != null && !Window.NextFrame())
			{
				ExitRequested = true;
				return;
			}

			base.Render(run);
		}

		public void Initialize()
		{
			_renderContext.Initialize();
		}
	}
}
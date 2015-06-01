using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;
using Grasshopper.SharpDX.Input;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class WindowRenderHost : RenderHost<IWindowRenderContext>, IWindowRenderHost
	{
		private readonly WindowRenderContext _renderContext;

		public WindowRenderHost(DeviceManager deviceManager, IInputContext input) : this(new WindowRenderContext(deviceManager, input))
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
using Grasshopper.Graphics;

namespace Grasshopper.SharpDX.Graphics
{
	public class Renderer : IRenderer
	{
		private readonly ViewportManager _viewportManager;
		private RendererContext _context;

		public Renderer(DeviceManager deviceManager, ViewportManager viewportManager)
		{
			_viewportManager = viewportManager;
			_context = new RendererContext(deviceManager, viewportManager);
		}

		public IAppWindow Window { get { return _viewportManager.Window; } }

		public bool Next(RenderFrameHandler run)
		{
			if(Window != null && !Window.NextFrame())
				return false;
			
			_context.MakeActive();
			
			return run(_context);
		}

		public void Initialize()
		{
			_context.Initialize();
		}

		public void Dispose()
		{
			_context.Dispose();
			_context = null;
		}
	}
}

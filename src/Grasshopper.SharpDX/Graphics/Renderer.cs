using Grasshopper.Graphics;

namespace Grasshopper.SharpDX.Graphics
{
	public class Renderer : IRenderer
	{
		private RendererContext _context;

		public Renderer(GraphicsContext graphicsContext, ViewportManager viewportManager)
		{
			ViewportManager = viewportManager;
			GraphicsContext = graphicsContext;
			_context = new RendererContext(graphicsContext, viewportManager);
		}

		public ViewportManager ViewportManager { get; private set; }
		public GraphicsContext GraphicsContext { get; private set; }

		public virtual bool Render(RenderFrameHandler run)
		{
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

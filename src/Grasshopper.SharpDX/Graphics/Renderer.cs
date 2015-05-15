using System;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class Renderer<T> : IRenderer<T>
		where T : class, IRendererContext
	{
		private T _context;

		public Renderer(GraphicsContext graphicsContext, ViewportManager viewportManager, T rendererContext)
		{
			ViewportManager = viewportManager;
			GraphicsContext = graphicsContext;
			_context = rendererContext;
		}

		public ViewportManager ViewportManager { get; private set; }
		public GraphicsContext GraphicsContext { get; private set; }

		public virtual bool Render(RenderFrameHandler<T> run)
		{
			_context.MakeActive();

			return run(_context);
		}

		public void Initialize()
		{
			_context.Initialize();
		}

		protected event Action Disposing;

		public void Dispose()
		{
			var handler = Disposing;
			if(handler != null)
				handler();

			_context.Dispose();
			_context = null;
		}
	}
}

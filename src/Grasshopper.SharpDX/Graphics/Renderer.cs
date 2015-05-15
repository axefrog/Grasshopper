using System;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class Renderer<T> : IRenderer<T>
		where T : class, IRendererContext
	{
		private T _context;

		public Renderer(T rendererContext)
		{
			_context = rendererContext;
		}

		public virtual bool Render(RenderFrameHandler<T> run)
		{
			_context.MakeActive();

			return run(_context);
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

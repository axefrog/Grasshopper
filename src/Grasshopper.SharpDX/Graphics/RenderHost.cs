using System;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics
{
	public class RenderHost<T> : IRenderHost<T>
		where T : class, IRenderContext
	{
		private T _context;

		public RenderHost(T renderContext)
		{
			_context = renderContext;
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

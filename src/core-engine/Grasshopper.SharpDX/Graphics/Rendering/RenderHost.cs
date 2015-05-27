using System;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class RenderHost<T> : IRenderHost<T>
		where T : class, IRenderContext
	{
		private T _context;
		
		public bool ExitRequested { get; protected set; }

		public RenderHost(T renderContext)
		{
			_context = renderContext;
		}

		public virtual void Render(RenderFrameHandler<T> frame)
		{
			_context.MakeActive();
			frame(_context);
			if(_context.ExitRequested)
				ExitRequested = true;
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

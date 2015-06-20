using System;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class RenderTarget<T> : IRenderTarget<T>
		where T : IDrawingContext
	{
		private T _targetContext;
		
		public bool Terminated { get; protected set; }

		public RenderTarget(T drawingContext)
		{
			_targetContext = drawingContext;
		}

		public virtual void Render(RenderFrameHandler<T> frame)
		{
			if(Terminated) return;
			_targetContext.Activate();
			frame(_targetContext);
		}

		protected event Action Disposing;

		public void Dispose()
		{
			var handler = Disposing;
			if(handler != null)
				handler();

			_targetContext.Dispose();
			_targetContext = default(T);
		}
	}
}

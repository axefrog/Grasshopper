using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IRenderTarget<T> : IDisposable
		where T : IDrawingContext
	{
		void Render(RenderFrameHandler<T> frame);
		bool Terminated { get; }
	}

	public delegate void RenderFrameHandler<T>(T context)
		where T : IDrawingContext;
}
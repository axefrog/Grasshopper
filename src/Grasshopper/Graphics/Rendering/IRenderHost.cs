using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IRenderHost<T> : IDisposable
		where T : IRenderContext
	{
		bool Render(RenderFrameHandler<T> run);
	}

	public delegate bool RenderFrameHandler<T>(T context)
		where T : IRenderContext;
}
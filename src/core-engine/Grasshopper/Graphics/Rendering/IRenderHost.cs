using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IRenderHost<T> : IDisposable
		where T : IRenderContext
	{
		void Render(RenderFrameHandler<T> frame);
		bool ExitRequested { get; }
	}

	public delegate void RenderFrameHandler<T>(T context)
		where T : IRenderContext;
}
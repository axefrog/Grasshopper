using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IRenderer<T> : IDisposable
		where T : IRendererContext
	{
		bool Render(RenderFrameHandler<T> run);
	}

	public delegate bool RenderFrameHandler<T>(T context)
		where T : IRendererContext;
}
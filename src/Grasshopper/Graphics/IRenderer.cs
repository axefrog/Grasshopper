using System;

namespace Grasshopper.Graphics
{
	public interface IRenderer : IDisposable
	{
		bool Next(RenderFrameHandler run);
		void Initialize();
		IAppWindow Window { get; }
	}

	public delegate bool RenderFrameHandler(IRendererContext context);

	public interface IRendererContext : IDisposable
	{
		IAppWindow Window { get; }
		void Initialize();
		void Clear(Color color);
		void Present();
	}
}
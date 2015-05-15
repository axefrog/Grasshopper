using Grasshopper.Graphics.Rendering;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics
{
	public class WindowRendererContext : RendererContext, IWindowRendererContext
	{
		private readonly ViewportManager _viewportManager;

		public WindowRendererContext(GraphicsContext graphicsContext, ViewportManager viewportManager) : base(graphicsContext, viewportManager.RenderTargetView)
		{
			_viewportManager = viewportManager;
		}

		public IAppWindow Window { get { return _viewportManager.Window; } }

		public void Present()
		{
			_viewportManager.SwapChain.Present(1, PresentFlags.None);
		}
	}
}
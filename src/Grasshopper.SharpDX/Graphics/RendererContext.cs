using Grasshopper.Graphics;
using SharpDX;
using SharpDX.DXGI;
using Color = Grasshopper.Graphics.Color;

namespace Grasshopper.SharpDX.Graphics
{
	public class RendererContext : IRendererContext
	{
		private readonly GraphicsContext _graphicsContext;
		private readonly ViewportManager _viewportManager;
		private bool _isDisposed;

		public RendererContext(GraphicsContext graphicsContext, ViewportManager viewportManager)
		{
			_graphicsContext = graphicsContext;
			_viewportManager = viewportManager;
		}

		public IAppWindow Window { get { return _viewportManager.Window; } }

		public void Initialize()
		{
			
		}

		public void MakeActive()
		{
			var dc = _graphicsContext.DeviceManager.Context;
			dc.OutputMerger.SetRenderTargets(_viewportManager.RenderTargetView);
		}

		public void Clear(Color color)
		{
			var dc = _graphicsContext.DeviceManager.Context;
			dc.ClearRenderTargetView(_viewportManager.RenderTargetView, new Color4(color.ToRgba()));
		}

		public void Present()
		{
			_viewportManager.SwapChain.Present(1, PresentFlags.None);
		}

		public void Dispose()
		{
			_isDisposed = true;
		}
	}
}
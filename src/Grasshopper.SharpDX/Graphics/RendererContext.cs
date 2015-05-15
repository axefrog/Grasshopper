using Grasshopper.Graphics.Rendering;
using SharpDX;
using SharpDX.Direct3D11;
using Color = Grasshopper.Graphics.Color;

namespace Grasshopper.SharpDX.Graphics
{
	public class RendererContext : IRendererContext
	{
		private readonly GraphicsContext _graphicsContext;
		private readonly RenderTargetView _renderTargetView;
		private bool _isDisposed;

		public RendererContext(GraphicsContext graphicsContext, RenderTargetView renderTargetView)
		{
			_graphicsContext = graphicsContext;
			_renderTargetView = renderTargetView;
		}

		public void Initialize()
		{
		}

		public void MakeActive()
		{
			var dc = _graphicsContext.DeviceManager.Context;
			dc.OutputMerger.SetRenderTargets(_renderTargetView);
		}

		public void Clear(Color color)
		{
			var dc = _graphicsContext.DeviceManager.Context;
			dc.ClearRenderTargetView(_renderTargetView, new Color4(color.ToRgba()));
		}

		public void Dispose()
		{
			_isDisposed = true;
		}
	}
}
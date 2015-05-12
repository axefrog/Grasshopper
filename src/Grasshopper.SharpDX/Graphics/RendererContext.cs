using System;
using Grasshopper.Graphics;
using SharpDX;
using SharpDX.DXGI;
using Color = Grasshopper.Graphics.Color;

namespace Grasshopper.SharpDX.Graphics
{
	public class RendererContext : IRendererContext
	{
		private readonly DeviceManager _deviceManager;
		private readonly ViewportManager _viewportManager;
		private bool _isDisposed;

		public RendererContext(DeviceManager deviceManager, ViewportManager viewportManager)
		{
			_deviceManager = deviceManager;
			_viewportManager = viewportManager;
		}

		public IAppWindow Window { get { return _viewportManager.Window; } }

		public void Initialize()
		{
			
		}

		public void MakeActive()
		{
			_deviceManager.Context.OutputMerger.SetRenderTargets(_viewportManager.RenderTargetView);
		}

		public void Clear(Color color)
		{
			AssertNotDisposed();
			_deviceManager.Context.ClearRenderTargetView(_viewportManager.RenderTargetView, new Color4(color.ToRgba()));
		}

		public void Present()
		{
			AssertNotDisposed();
			_viewportManager.SwapChain.Present(1, PresentFlags.None);
		}

		private void AssertNotDisposed()
		{
			if(_isDisposed)
				throw new ObjectDisposedException("Cannot call method; renderer has been disposed");
		}

		public void Dispose()
		{
			_isDisposed = true;
		}
	}
}
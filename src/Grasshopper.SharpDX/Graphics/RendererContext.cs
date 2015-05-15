using System;
using Grasshopper.Graphics.Rendering;
using SharpDX;
using SharpDX.Direct3D11;
using Color = Grasshopper.Graphics.Color;

namespace Grasshopper.SharpDX.Graphics
{
	public abstract class RendererContext : IRendererContext
	{
		private readonly DeviceManager _deviceManager;
		private RenderTargetView _renderTargetView;

		protected RendererContext(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected void SetRenderTargetView(RenderTargetView renderTargetView)
		{
			_renderTargetView = renderTargetView;
		}

		public void MakeActive()
		{
			var dc = _deviceManager.Context;
			dc.OutputMerger.SetRenderTargets(_renderTargetView);
		}

		public void Clear(Color color)
		{
			var dc = _deviceManager.Context;
			dc.ClearRenderTargetView(_renderTargetView, new Color4(color.ToRgba()));
		}

		protected event Action Disposing;
		protected bool IsDisposed { get; private set; }

		public void Dispose()
		{
			var handler = Disposing;
			if(handler != null)
				handler();

			IsDisposed = true;
		}
	}
}
using System;
using Grasshopper.Graphics.Rendering;
using SharpDX;
using SharpDX.Direct3D11;
using Color = Grasshopper.Graphics.Color;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public abstract class RenderContext : IRenderContext
	{
		private readonly DeviceManager _deviceManager;
		private RenderTargetView _renderTargetView;

		protected RenderContext(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected void SetRenderTargetView(RenderTargetView renderTargetView)
		{
			_renderTargetView = renderTargetView;
		}

		public void MakeActive()
		{
			if(_renderTargetView == null)
				throw new InvalidOperationException("The implementation of this class forgot to call SetRenderTargetView during initialization");
			var dc = _deviceManager.Context;
			dc.OutputMerger.SetRenderTargets(_renderTargetView);
		}

		public void Exit()
		{
			ExitRequested = true;
		}

		public bool ExitRequested { get; private set; }

		public void Clear(Color color)
		{
			var dc = _deviceManager.Context;
			dc.ClearRenderTargetView(_renderTargetView, new Color4(color.ToRgba()));
		}

		public void Draw(VertexBufferLocation loc, DrawType drawType = DrawType.Triangles)
		{
			_deviceManager.Context.DrawIndexed(loc.IndexCount, loc.IndexBufferOffset, loc.VertexBufferOffset);
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
using System;
using System.Windows.Forms;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;
using Grasshopper.SharpDX.Graphics.Rendering.Internal;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Color = Grasshopper.Graphics.Primitives.Color;
using ResultCode = SharpDX.DXGI.ResultCode;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class WindowDrawingContext : DrawingContext, IWindowDrawingContext
	{
		private readonly AppWindow _window;
		private readonly DepthBuffer _depthBuffer;
		private readonly WindowTextureBuffer _windowTextureBuffer;

		public WindowDrawingContext(GraphicsContext graphics, IInputContext input) : base(graphics.DeviceManager)
		{
			Graphics = graphics;

			_window = new AppWindow(input);
			_window.SizeChanged += win => Initialize();

			_depthBuffer = new DepthBuffer(DeviceManager, _window.ClientWidth, _window.ClientHeight);
			_windowTextureBuffer = new WindowTextureBuffer(DeviceManager, _window.Form.Handle, _window.ClientWidth, _window.ClientHeight);
		}

		public IGraphicsContext Graphics { get; private set; }
		public IAppWindow Window { get { return _window; } }

		public void Initialize()
		{
			if(_window.Form.WindowState == FormWindowState.Minimized)
				return;

			if(!DeviceManager.IsInitialized)
				throw new InvalidOperationException("Device manager is not initialized");

			DestroyResources();

			_depthBuffer.Resize(Window.ClientWidth, Window.ClientHeight);
			_windowTextureBuffer.Resize(Window.ClientWidth, Window.ClientHeight);
			
			_depthBuffer.Initialize();
			_windowTextureBuffer.Initialize();

			var viewport = new ViewportF(0, 0, Window.ClientWidth, Window.ClientHeight);
			DeviceManager.Context.Rasterizer.SetViewport(viewport);
		}

		protected override void DestroyResources()
		{
			_depthBuffer.Uninitialize();
			_windowTextureBuffer.Uninitialize();
		}

		public void Present()
		{
			try
			{
				_windowTextureBuffer.SwapChain.Present(1, PresentFlags.None);
			}
			catch(SharpDXException ex)
			{
				if(ex.ResultCode == ResultCode.DeviceRemoved || ex.ResultCode == ResultCode.DeviceReset)
					DeviceManager.Initialize();
				else
					throw;
			}
		}

		protected override void MakeTargetsActive()
		{
			DeviceManager.Context.OutputMerger.SetTargets(_depthBuffer.DepthStencilView, _windowTextureBuffer.RenderTargetView);
		}

		public override void Clear(Color color)
		{
			if(_depthBuffer.DepthStencilView != null)
				DeviceManager.Context.ClearDepthStencilView(_depthBuffer.DepthStencilView, DepthStencilClearFlags.Depth, 1f, 0);

			if(_windowTextureBuffer.RenderTargetView != null)
				DeviceManager.Context.ClearRenderTargetView(_windowTextureBuffer.RenderTargetView, new Color4(color.ToRgba()));
		}
	}
}
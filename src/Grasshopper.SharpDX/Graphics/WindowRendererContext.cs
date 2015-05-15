using System;
using Grasshopper.Graphics.Rendering;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device2 = SharpDX.DXGI.Device2;
using Resource = SharpDX.Direct3D11.Resource;

namespace Grasshopper.SharpDX.Graphics
{
	public class WindowRendererContext : RendererContext, IWindowRendererContext
	{
		private readonly AppWindow _window;
		private readonly DeviceManager _deviceManager;

		public WindowRendererContext(DeviceManager deviceManager, AppWindow window) : base(deviceManager)
		{
			_deviceManager = deviceManager;
			_window = window;

			_window.SizeChanged += win => Initialize();
		}

		public IAppWindow Window { get { return _window; } }
		public SwapChain1 SwapChain { get; private set; }
		public Texture2D BackBuffer { get; private set; }
		public RenderTargetView RenderTargetView { get; private set; }

		public void Initialize()
		{
			if(!_deviceManager.IsInitialized) throw new InvalidOperationException("Device manager is not initialized");
			Console.WriteLine("Initializing Viewport");

			DestroyResources();

			var swapChainDescription = new SwapChainDescription1
			{
				Width = Window.ClientWidth,
				Height = Window.ClientHeight,
				Format = Format.B8G8R8A8_UNorm,
				Stereo = false,
				SampleDescription = new SampleDescription(4, 4),
				Usage = Usage.BackBuffer | Usage.RenderTargetOutput,
				BufferCount = 1,
				Scaling = Scaling.Stretch,
				SwapEffect = SwapEffect.Discard,
				Flags = SwapChainFlags.AllowModeSwitch
			};
			var fullScreenDescription = new SwapChainFullScreenDescription
			{
				RefreshRate = new Rational(60, 1),
				Scaling = DisplayModeScaling.Centered,
				Windowed = true
			};

			using(var dxgiDevice2 = _deviceManager.Device.QueryInterface<Device2>())
			using(var dxgiFactory2 = dxgiDevice2.Adapter.GetParent<Factory2>())
			{
				SwapChain = new SwapChain1(dxgiFactory2, _deviceManager.Device, _window.Form.Handle, ref swapChainDescription, fullScreenDescription);
				if(_window != null)
					dxgiFactory2.MakeWindowAssociation(_window.Form.Handle, WindowAssociationFlags.IgnoreAll);
			}
			BackBuffer = Resource.FromSwapChain<Texture2D>(SwapChain, 0);
			RenderTargetView = new RenderTargetView(_deviceManager.Device, BackBuffer);
		}

		private void DestroyResources()
		{
			if(RenderTargetView != null)
			{
				RenderTargetView.Dispose();
				RenderTargetView = null;
			}
			if(BackBuffer != null)
			{
				BackBuffer.Dispose();
				BackBuffer = null;
			}
			if(SwapChain != null)
			{
				SwapChain.Dispose();
				SwapChain = null;
			}
		}

		public void Present()
		{
			SwapChain.Present(1, PresentFlags.None);
		}
	}
}
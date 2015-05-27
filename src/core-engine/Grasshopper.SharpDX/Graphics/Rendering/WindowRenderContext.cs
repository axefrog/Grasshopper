using System;
using Grasshopper.Graphics.Rendering;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device2 = SharpDX.DXGI.Device2;
using Resource = SharpDX.Direct3D11.Resource;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public class WindowRenderContext : RenderContext, IWindowRenderContext
	{
		private readonly AppWindow _window;
		private readonly DeviceManager _deviceManager;

		public WindowRenderContext(DeviceManager deviceManager) : base(deviceManager)
		{
			_deviceManager = deviceManager;
			_window = new AppWindow();

			_window.SizeChanged += win => Initialize();
			_window.Closed += win => Exit();
			Disposing += DestroyResources;
		}

		public IAppWindow Window { get { return _window; } }
		public SwapChain1 SwapChain { get; private set; }
		public Texture2D BackBuffer { get; private set; }
		public RenderTargetView BackBufferView { get; private set; }
		public DepthStencilView DepthStencilView { get; private set; }

		public void Initialize()
		{
			if(!_deviceManager.IsInitialized) throw new InvalidOperationException("Device manager is not initialized");

			DestroyResources();
			var sampleDescription = new SampleDescription(4, 4);

			var swapChainDescription = new SwapChainDescription1
			{
				Width = Window.ClientWidth,
				Height = Window.ClientHeight,
				Format = Format.B8G8R8A8_UNorm,
				Stereo = false,
				SampleDescription = sampleDescription,
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
			var zBufferTextureDescription = new Texture2DDescription
			{
				Format = Format.D16_UNorm,
				ArraySize = 1,
				MipLevels = 1,
				Width = Window.ClientWidth,
				Height = Window.ClientHeight,
				SampleDescription = sampleDescription,
				Usage = ResourceUsage.Default,
				BindFlags = BindFlags.DepthStencil,
				CpuAccessFlags = CpuAccessFlags.None,
				OptionFlags = ResourceOptionFlags.None
			};
			using(var zBufferTexture = new Texture2D(_deviceManager.Device, zBufferTextureDescription))
				DepthStencilView = new DepthStencilView(_deviceManager.Device, zBufferTexture);

			using(var dxgiDevice2 = _deviceManager.Device.QueryInterface<Device2>())
			using(var dxgiFactory2 = dxgiDevice2.Adapter.GetParent<Factory2>())
			{
				SwapChain = new SwapChain1(dxgiFactory2, _deviceManager.Device, _window.Form.Handle, ref swapChainDescription, fullScreenDescription);
				if(_window != null)
					dxgiFactory2.MakeWindowAssociation(_window.Form.Handle, WindowAssociationFlags.IgnoreAll);
			}
			BackBuffer = Resource.FromSwapChain<Texture2D>(SwapChain, 0);
			BackBufferView = new RenderTargetView(_deviceManager.Device, BackBuffer);
			SetRenderTargetView(BackBufferView);
			SetDepthStencilView(DepthStencilView);

			var viewport = new ViewportF(0, 0, Window.ClientWidth, Window.ClientHeight);
			_deviceManager.Context.Rasterizer.SetViewport(viewport);
		}

		private void DestroyResources()
		{
			if(BackBufferView != null)
			{
				BackBufferView.Dispose();
				BackBufferView = null;
			}
			if(DepthStencilView != null)
			{
				DepthStencilView.Dispose();
				DepthStencilView = null;
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
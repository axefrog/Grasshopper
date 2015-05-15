using System;
using Grasshopper.Graphics.Rendering;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device2 = SharpDX.DXGI.Device2;
using Resource = SharpDX.Direct3D11.Resource;

namespace Grasshopper.SharpDX.Graphics
{
	/// <summary>
	/// The viewport manager encapsulates functionality for the management of resources for a texture surface that
	/// is intended to be rendered to. This will usually be a window backbuffer or a dynamically-written texture.
	/// </summary>
	public class ViewportManager : IDisposable
	{
		private readonly AppWindow _window;
		private readonly DeviceManager _deviceManager;

		public ViewportManager(IAppWindow window, DeviceManager deviceManager)
		{
			if(window == null) throw new ArgumentNullException("window");
			if(deviceManager == null) throw new ArgumentNullException("deviceManager");

			_window = window as AppWindow;
			if(Window == null) throw new ArgumentException("The window object must be an instance of AppWindow");
			_deviceManager = deviceManager;

			Window.SizeChanged += win => Initialize();
		}

		public SwapChain1 SwapChain { get; private set; }
		public Texture2D BackBuffer { get; private set; }
		public RenderTargetView RenderTargetView { get; private set; }

		public AppWindow Window
		{
			get { return _window; }
		}

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
				SwapChain = new SwapChain1(dxgiFactory2, _deviceManager.Device, Window.Form.Handle, ref swapChainDescription, fullScreenDescription);
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

		public void Dispose()
		{
			DestroyResources();
		}
	}
}
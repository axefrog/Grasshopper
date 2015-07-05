using System;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device2 = SharpDX.DXGI.Device2;
using Resource = SharpDX.Direct3D11.Resource;

namespace Grasshopper.SharpDX.Graphics.Rendering.Internal
{
    internal class WindowTextureBuffer : IDisposable
    {
        private readonly DeviceManager _deviceManager;
        private readonly IntPtr _windowHandle;

        public WindowTextureBuffer(DeviceManager deviceManager, IntPtr windowHandle, int width, int height)
        {
            _deviceManager = deviceManager;
            _windowHandle = windowHandle;
            Width = width;
            Height = height;

            SampleCount = 4;
            SampleQuality = 4;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Initialized { get; private set; }
        public RenderTargetView RenderTargetView { get; private set; }
        public Texture2D Texture { get; private set; }
        public SwapChain SwapChain { get; private set; }

        public int SampleCount { get; set; }
        public int SampleQuality { get; set; }

        public void Initialize()
        {
            if(!_deviceManager.IsInitialized)
                throw new InvalidOperationException("Device manager is not initialized");

            if(Initialized)
                Uninitialize();

            var swapChainDescription = new SwapChainDescription1
            {
                Width = Width,
                Height = Height,
                Format = Format.B8G8R8A8_UNorm,
                Stereo = false,
                SampleDescription = new SampleDescription(SampleCount, SampleQuality),
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
                SwapChain = new SwapChain1(dxgiFactory2, _deviceManager.Device, _windowHandle, ref swapChainDescription, fullScreenDescription);
                dxgiFactory2.MakeWindowAssociation(_windowHandle, WindowAssociationFlags.IgnoreAll);
            }

            Texture = Resource.FromSwapChain<Texture2D>(SwapChain, 0);
            RenderTargetView = new RenderTargetView(_deviceManager.Device, Texture);

            Initialized = true;
        }

        public void Uninitialize()
        {
            if(Texture != null)
            {
                Texture.Dispose();
                Texture = null;
            }
            if(RenderTargetView != null)
            {
                RenderTargetView.Dispose();
                RenderTargetView = null;
            }
            if(SwapChain != null)
            {
                SwapChain.Dispose();
                SwapChain = null;
            }
            Initialized = false;
        }

        public void Resize(int width, int height)
        {
            Width = width;
            Height = height;

            if(Initialized)
                Initialize();
        }

        public void Dispose()
        {
            Uninitialize();
        }
    }
}
using System;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics.Rendering.Internal
{
    internal class DepthBuffer : IDisposable
    {
        private readonly DeviceManager _deviceManager;
        private Texture2DDescription _zBufferTextureDescription;

        public DepthBuffer(DeviceManager deviceManager, int width, int height)
        {
            _deviceManager = deviceManager;
            Width = width;
            Height = height;

            SampleCount = 4;
            SampleQuality = 4;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Initialized { get; private set; }
        internal DepthStencilView DepthStencilView { get; private set; }
        
        public int SampleCount { get; set; }
        public int SampleQuality { get; set; }

        public void Initialize()
        {
            if(!_deviceManager.IsInitialized)
                throw new InvalidOperationException("Device manager is not initialized");

            if(Initialized)
                Uninitialize();

            _zBufferTextureDescription = new Texture2DDescription
            {
                Format = Format.D32_Float_S8X24_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = Width,
                Height = Height,
                SampleDescription = new SampleDescription(SampleCount, SampleQuality),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };
            using(var zBufferTexture = new Texture2D(_deviceManager.Device, _zBufferTextureDescription))
                DepthStencilView = new DepthStencilView(_deviceManager.Device, zBufferTexture);

            Initialized = true;
        }

        public void Uninitialize()
        {
            if(DepthStencilView != null)
            {
                DepthStencilView.Dispose();
                DepthStencilView = null;
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

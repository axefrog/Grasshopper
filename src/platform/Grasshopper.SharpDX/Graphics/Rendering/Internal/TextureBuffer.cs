using System;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics.Rendering.Internal
{
    internal class TextureBuffer : IDisposable
    {
        private readonly DeviceManager _deviceManager;

        public TextureBuffer(DeviceManager deviceManager, int width, int height)
        {
            _deviceManager = deviceManager;
            Width = width;
            Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Initialized { get; private set; }
        public ShaderResourceView ShaderResourceView { get; private set; }
        public Texture2D Texture { get; private set; }
        
        public bool UseMipMaps { get; set; }
        public int SampleCount { get; set; }
        public int SampleQuality { get; set; }

        public void Initialize()
        {    
            if(!_deviceManager.IsInitialized)
                throw new InvalidOperationException("Device manager is not initialized");

            if(Initialized)
                Uninitialize();

            var textureDescription = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.None,
                ArraySize = 1,
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                Height = Width,
                Width = Height,
                SampleDescription = new SampleDescription(SampleCount, SampleQuality),
                MipLevels = UseMipMaps ? 0 : 1,
                Format = Format.B8G8R8A8_UNorm,
            };

            Texture = new Texture2D(_deviceManager.Device, textureDescription);
            new RenderTargetView(_deviceManager.Device, Texture);
            ShaderResourceView = new ShaderResourceView(_deviceManager.Device, Texture);
        }

        public void Uninitialize()
        {
            if(Texture != null)
            {
                Texture.Dispose();
                Texture = null;
            }
            if(ShaderResourceView != null)
            {
                ShaderResourceView.Dispose();
                ShaderResourceView = null;
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
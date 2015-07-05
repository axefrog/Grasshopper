using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics.Materials
{
    class DynamicTexture2DResource : ShaderResource, IDynamicTexture2DResource
    {
        private readonly PixelFormat _pixelFormat;
        private Texture2DDescription _descr;
        private Texture2D _texture;

        public DynamicTexture2DResource(DeviceManager deviceManager, string id, int width, int height, PixelFormat pixelFormat) : base(deviceManager, id)
        {
            _pixelFormat = pixelFormat;
            Width = width;
            Height = height;
        }

        public TextureType TextureType { get { return TextureType.Texture2D; } }
        public TextureDataSource DataSource { get { return TextureDataSource.Dynamic; } }
        public PixelFormat PixelFormat { get { return _pixelFormat; } }
        public int Width { get; private set; }
        public int Height { get; private set; }

        protected override void InitializeInternal()
        {
            _descr = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.Write,
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                Height = Width,
                Width = Height,
                SampleDescription = new SampleDescription(1, 0),
                MipLevels = 1,
                Format = _pixelFormat.ToDXGIFormat(),
                Usage = ResourceUsage.Dynamic
            };
            _texture = new Texture2D(DeviceManager.Device, _descr);
            ShaderResourceView = new ShaderResourceView(DeviceManager.Device, _texture);
        }

        protected override void UninitializeInternal()
        {
            if(ShaderResourceView != null)
            {
                ShaderResourceView.Dispose();
                ShaderResourceView = null;
            }
            if(_texture != null)
            {
                _texture.Dispose();
                _texture = null;
                _descr = default(Texture2DDescription);
            }
        }

        public IShaderResourceWriter BeginWrite()
        {
            return new ShaderResourceWriter<Texture2D>(DeviceManager, _texture);
        }
    }
}
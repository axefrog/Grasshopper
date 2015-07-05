using System;
using Grasshopper.Graphics.Materials;
using Grasshopper.Platform;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
    class Texture2DFileResource : ShaderResource, ITexture2DResource
    {
        private readonly IFileSource _fileSource;
        private Texture2DDescription _descr;

        public Texture2DFileResource(DeviceManager deviceManager, string id, IFileSource fileSource)
            : base(deviceManager, id)
        {
            if(fileSource == null) throw new ArgumentNullException("fileSource");
            _fileSource = fileSource;
        }

        public TextureType TextureType { get { return TextureType.Texture2D; } }
        public TextureDataSource DataSource { get { return TextureDataSource.FileSystem; } }
        public PixelFormat PixelFormat { get { return _descr.Format.ToPixelFormat(); } }
        public int Width { get { return _descr.Width; } }
        public int Height { get { return _descr.Height; } }

        protected override void InitializeInternal()
        {
            // todo: make sure the texture has actually loaded successfully - note that it may not be loading from a regular file, so we can't rely on FileNotFoundException bubbling up; wrap it in a local exception
            using(var stream = _fileSource.OpenRead())
                ShaderResourceView = ShaderResourceView.FromStream(DeviceManager.Device, stream, _fileSource.Size);
            _descr = ShaderResourceView.ResourceAs<Texture2D>().Description;
        }

        protected override void UninitializeInternal()
        {
            _descr = default(Texture2DDescription);
            if(ShaderResourceView != null)
            {
                ShaderResourceView.Dispose();
                ShaderResourceView = null;
            }
        }
    }
}
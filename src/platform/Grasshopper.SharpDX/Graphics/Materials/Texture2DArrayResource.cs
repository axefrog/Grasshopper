using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics.Materials
{
    class Texture2DArrayResource : ShaderResource, ITextureResource
    {
        private readonly Texture2DFileResource[] _textures;
        private Texture2D _textureArray;
        private Format _format = Format.Unknown;

        public Texture2DArrayResource(DeviceManager deviceManager, string id, IEnumerable<Texture2DFileResource> sourceTextures)
            : base(deviceManager, id)
        {
            if(sourceTextures == null) throw new ArgumentNullException("sourceTextures");
            _textures = sourceTextures.ToArray();
        }

        public TextureType TextureType { get { return TextureType.Texture2DArray; } }
        public TextureDataSource DataSource { get { return TextureDataSource.Internal; } }
        public PixelFormat PixelFormat { get { return _format.ToPixelFormat(); } }

        protected override void InitializeInternal()
        {
            var sourceTextures = _textures.Select(t =>
            {
                var view = t.ShaderResourceView;
                if(view == null)
                    throw new InvalidOperationException(string.Format("Texture array cannot be created because source texture '{0}' is not initialized", t.Id));
                return view.Resource.QueryInterface<Texture2D>();
            }).ToArray();

            var descr = sourceTextures[0].Description;
            descr.ArraySize = _textures.Length;
            _format = descr.Format;
            _textureArray = new Texture2D(DeviceManager.Device, descr);
            ShaderResourceView = new ShaderResourceView(DeviceManager.Device, _textureArray);

            var mipLevels = descr.MipLevels;
            for(var i = 0; i < mipLevels; i++)
            {
                for(var j = 0; j < _textures.Length; j++)
                {
                    var texture = sourceTextures[j];
                    DeviceManager.Context.CopySubresourceRegion(texture, i, null, _textureArray, mipLevels * j + i);
                }
            }
        }

        protected override void UninitializeInternal()
        {
            if(ShaderResourceView != null)
            {
                ShaderResourceView.Dispose();
                ShaderResourceView = null;
            }
            if(_textureArray != null)
            {
                _textureArray.Dispose();
                _textureArray = null;
            }
        }
    }
}
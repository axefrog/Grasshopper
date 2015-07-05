using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Materials;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Materials
{
    class TextureResourceManager : IndexActivatablePlatformResourceManager<ITextureResource>, ITextureResourceManager
    {
        private readonly DeviceManager _deviceManager;
        private readonly Lazy<IFileStore> _fileStore;

        public TextureResourceManager(DeviceManager deviceManager, Lazy<IFileStore> fileStore)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            if(fileStore == null) throw new ArgumentNullException("fileStore");

            _deviceManager = deviceManager;
            _fileStore = fileStore;
        }

        protected override void Activate(int firstIndex, IEnumerable<ITextureResource> resources)
        {
            _deviceManager.Context.PixelShader.SetShaderResources(firstIndex, resources.Select((r, i) =>
            {
                var texture = (ShaderResource)r;
                texture.SetActivatedExternally(firstIndex + i);
                return texture.ShaderResourceView;
            }).ToArray());
        }

        public ITextureResource Create2DFromFile(string id, string path)
        {
            var assetSource = _fileStore.Value.GetFile(path);
            return Create2DFromFile(id, assetSource);
        }

        public ITextureResource Create2DFromFile(string id, IFileSource fileSource)
        {
            return Add(new Texture2DFileResource(_deviceManager, id, fileSource));
        }

        public IDynamicTexture2DResource Create2DDynamic(string id, int width, int height, PixelFormat pixelFormat)
        {
            return (DynamicTexture2DResource)Add(new DynamicTexture2DResource(_deviceManager, id, width, height, pixelFormat));
        }

        public ITextureResource Create2DArray(string textureArrayId, params string[] sourceTextureIds)
        {
            return Add(new Texture2DArrayResource(_deviceManager, textureArrayId, sourceTextureIds.Select(id =>
            {
                var sourceTexture = this[id] as Texture2DFileResource;
                if(sourceTexture == null)
                    throw new InvalidOperationException(string.Format("Texture resource '{0}' cannot be used in a texture array as it is of type {1}", textureArrayId, this[id].GetType().FullName));
                return sourceTexture;
            })));
        }
    }
}

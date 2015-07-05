using System;
using System.Collections.Generic;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
    public interface ITextureResourceManager : IIndexActivatablePlatformResourceManager<ITextureResource>
    {
        ITextureResource Create2DFromFile(string id, string path);
        ITextureResource Create2DFromFile(string id, IFileSource fileSource);
        IDynamicTexture2DResource Create2DDynamic(string id, int width, int height, PixelFormat pixelFormat = PixelFormat.R8G8B8A8_UNorm);
        ITextureResource Create2DArray(string textureArrayId, params string[] sourceTextureIds);
    }
}
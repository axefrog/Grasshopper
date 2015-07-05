using System;
using System.IO;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
    public interface ITextureResource : IIndexActivatablePlatformResource
    {
        TextureType TextureType { get; }
        TextureDataSource DataSource { get; }
        PixelFormat PixelFormat { get; }
    }

    public interface ITexture1DResource : ITextureResource
    {
        int Size { get; }
    }

    public interface ITexture2DResource : ITextureResource
    {
        int Width { get; }
        int Height { get; }
    }

    public interface IDynamicTexture2DResource : ITexture2DResource
    {
        IShaderResourceWriter BeginWrite();
    }

    public interface IShaderResourceWriter : IDisposable
    {
        void Write<T>(T value) where T : struct;
        void WriteRange<T>(T[] values) where T : struct;
        void Seek(long offset, SeekOrigin origin);
    }

    //public interface IRenderTargetTexture : ITexture2DResource
    //{
    //}

    public interface ITexture2DArray : ITexture2DResource
    {
    }

    public enum TextureType
    {
        //Texture1D,
        Texture2D,
        //Texture3D,
        //Texture1DArray,
        Texture2DArray,
        //TextureCube
    }

    public enum TextureDataSource
    {
        /// <summary>
        /// The pixel data comes from the file system
        /// </summary>
        FileSystem,
        /// <summary>
        /// The pixel data is written directly by the software
        /// </summary>
        Dynamic,
        /// <summary>
        /// The pixel data results from draw calls to a render target
        /// </summary>
        RenderTarget,
        /// <summary>
        /// The pixel data is acquired internally by the system from reference sources
        /// </summary>
        Internal
    }
}
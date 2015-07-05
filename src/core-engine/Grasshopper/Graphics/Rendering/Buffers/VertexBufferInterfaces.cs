using System;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
    public interface IVertexBufferManagerFactory
    {
        IVertexBufferManager<TVertex> Create<TVertex>()
            where TVertex : struct;
    }

    public interface IVertexBufferManager<TVertex> : IIndexActivatablePlatformResourceManager<IVertexBufferResource<TVertex>>
        where TVertex : struct
    {
        IVertexBufferResource<TVertex> Create(string id);
    }

    public interface IVertexBufferResource<TVertex> : IIndexActivatablePlatformResource
        where TVertex : struct
    {
        IVertexBufferDataWriter<TVertex> BeginWrite(int totalItemsInBuffer);
    }

    public interface IVertexBufferDataWriter<TVertex> : IDisposable
    {
        string Id { get; }
        int TotalVertices { get; }
        long Position { get; set; }
        long Length { get; }
        void Write(ref TVertex data);
        void Write(TVertex[] data);
    }
}
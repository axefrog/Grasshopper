using System.Collections.Generic;
using Grasshopper.Graphics.SceneManagement;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
    public class MeshBufferManager<T> : ActivatablePlatformResourceManager<MeshBuffer<T>> where T : struct
    {
        private readonly IVertexBufferManager<T> _vertexBufferManager;
        private readonly IIndexBufferManager _indexBufferManager;
        private readonly Dictionary<string, MeshBuffer<T>> _meshBuffers = new Dictionary<string, MeshBuffer<T>>();

        public MeshBufferManager(IGraphicsContext graphicsContext)
        {
            _vertexBufferManager = graphicsContext.VertexBufferManagerFactory.Create<T>();
            _indexBufferManager = graphicsContext.IndexBufferManagerFactory.Create();
            Disposed += OnDisposed;
        }

        public MeshBuffer<T> Create(string id)
        {
            return Create(id, new Mesh<T>[0]);
        }

        public MeshBuffer<T> Create(string id, params Mesh<T>[] meshes)
        {
            return Create(id, (IEnumerable<Mesh<T>>)meshes);
        }

        public MeshBuffer<T> Create(string id, IEnumerable<Mesh<T>> meshes)
        {
            return Add(new MeshBuffer<T>(id, _vertexBufferManager, _indexBufferManager, meshes));
        }

        private void OnDisposed()
        {
            _vertexBufferManager.Dispose();
            _indexBufferManager.Dispose();
        }
    }
}
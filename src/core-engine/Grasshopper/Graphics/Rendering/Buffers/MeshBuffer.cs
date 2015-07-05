using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.SceneManagement;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
    public class MeshBuffer<T> : ActivatablePlatformResource where T : struct
    {
        private readonly IVertexBufferManager<T> _vertexBufferManager;
        private readonly IIndexBufferManager _indexBufferManager;
        private IVertexBufferResource<T> _vertexBuffer;
        private IIndexBufferResource _indexBuffer;
        private readonly Dictionary<string, MeshLocation> _locations = new Dictionary<string, MeshLocation>();

        public MeshBuffer(string id, IVertexBufferManager<T> vertexBufferManager, IIndexBufferManager indexBufferManager, IEnumerable<Mesh<T>> meshes)
            : base(id)
        {
            _vertexBufferManager = vertexBufferManager;
            _indexBufferManager = indexBufferManager;
            Meshes = meshes;
        }

        public IEnumerable<Mesh<T>> Meshes { get; set; }

        protected override void InitializeInternal()
        {
            _vertexBuffer = _vertexBufferManager.Create(Id);
            _indexBuffer = _indexBufferManager.Create(Id);

            var vertexCount = Meshes.Select(m => m.Vertices.Length).Sum();
            var indexCount = Meshes.Select(m => m.Indices.Length).Sum();

            using(var vertexWriter = _vertexBuffer.BeginWrite(vertexCount))
            using(var indexWriter = _indexBuffer.BeginWrite(indexCount))
            {
                int vbOffset = 0, ibOffset = 0;
                foreach(var mesh in Meshes)
                {
                    _locations.Add(mesh.Id, new MeshLocation(mesh.Indices.Length, vbOffset, ibOffset, mesh.DrawType));
                    for(var i = 0; i < mesh.Vertices.Length; i++)
                        vertexWriter.Write(ref mesh.Vertices[i]);
                    foreach(var index in mesh.Indices)
                        indexWriter.Write(index);
                    vbOffset += mesh.Vertices.Length;
                    ibOffset += mesh.Indices.Length;
                }
            }
        }

        protected override void UninitializeInternal()
        {
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
        }

        protected override void SetActive()
        {
            _vertexBuffer.Activate(0);
            _indexBuffer.Activate();
        }

        public MeshLocation this[string id]
        {
            get
            {
                MeshLocation loc;
                if(!_locations.TryGetValue(id, out loc))
                    throw new ArgumentOutOfRangeException("id", "The specified mesh does not exist in this buffer");
                return loc;
            }
        }
    }
}
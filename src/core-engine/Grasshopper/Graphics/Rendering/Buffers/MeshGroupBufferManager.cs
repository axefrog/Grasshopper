using System;
using System.Collections.Generic;
using System.Linq;
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
			var buffer = new MeshBuffer<T>(id, _vertexBufferManager, _indexBufferManager);
			buffer.Meshes = meshes;
			buffer.Initialize();
			return buffer;
		}

		protected override MeshBuffer<T> CreateResource(string id)
		{
			return new MeshBuffer<T>(id, _vertexBufferManager, _indexBufferManager);
		}

		private void OnDisposed()
		{
			_vertexBufferManager.Dispose();
			_indexBufferManager.Dispose();
		}
	}

	public class MeshBuffer<T> : ActivatablePlatformResource where T : struct
	{
		private readonly IVertexBufferManager<T> _vertexBufferManager;
		private readonly IIndexBufferManager _indexBufferManager;
		private IVertexBufferResource<T> _vertexBuffer;
		private IIndexBufferResource _indexBuffer;
		private readonly Dictionary<string, MeshLocation> _locations = new Dictionary<string, MeshLocation>();

		public MeshBuffer(string id, IVertexBufferManager<T> vertexBufferManager, IIndexBufferManager indexBufferManager)
			: base(id)
		{
			_vertexBufferManager = vertexBufferManager;
			_indexBufferManager = indexBufferManager;
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

	/// <summary>
	/// Represents location information required to draw a specific mesh stored within a packed vertex buffer
	/// </summary>
	public class MeshLocation
	{
		public MeshLocation(int indexCount, int vertexBufferOffset, int indexBufferOffset, DrawType drawType)
		{
			IndexCount = indexCount;
			VertexBufferOffset = vertexBufferOffset;
			IndexBufferOffset = indexBufferOffset;
			DrawType = drawType;
		}

		/// <summary>
		/// The number of indices required to draw the mesh at this location
		/// </summary>
		public int IndexCount { get; private set; }
		/// <summary>
		/// The position of the first vertex representing this mesh in the buffer
		/// </summary>
		public int VertexBufferOffset { get; private set; }
		/// <summary>
		/// The position of the vertex index representing this mesh's triangles in the buffer
		/// </summary>
		public int IndexBufferOffset { get; private set; }
		/// <summary>
		/// The type of graphics primitives to draw
		/// </summary>
		public DrawType DrawType { get; set; }
	}
}

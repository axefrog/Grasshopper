using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Geometry.Primitives;
using Grasshopper.Graphics.Rendering;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class MeshGroupBuffer : IDisposable
	{
		private readonly MeshGroup _meshes;
		private readonly Dictionary<string, VertexBufferLocation> _locations = new Dictionary<string, VertexBufferLocation>();
		private static readonly int _sizeofVertex = Utilities.SizeOf<Vertex>();
		private static readonly int _sizeofIndex = Utilities.SizeOf<int>();

		public MeshGroupBuffer(MeshGroup meshes)
		{
			_meshes = meshes;
		}

		public VertexBufferBinding VertexBufferBinding { get; private set; }
		public Buffer VertexBuffer { get; private set; }
		public Buffer IndexBuffer { get; private set; }

		public void Initialize(DeviceManager deviceManager)
		{
			var vertexCount = _meshes.SelectMany(m => m.Vertices).Count();
			var indexCount = _meshes.SelectMany(m => m.Indices).Count();
			using(var vertexBufferStream = new DataStream(_sizeofVertex * vertexCount, true, true))
			using(var indexBufferStream = new DataStream(_sizeofIndex * indexCount, true, true))
			{
				int vbOffset = 0, ibOffset = 0;
				foreach(var mesh in _meshes)
				{
					_locations.Add(mesh.Id, new VertexBufferLocation(mesh.Indices.Length, vbOffset, ibOffset));
					foreach(var vertex in mesh.Vertices)
						vertexBufferStream.Write(vertex);
					foreach(var index in mesh.Indices)
						indexBufferStream.Write(index);
					vbOffset += mesh.Vertices.Length;
					ibOffset += mesh.Indices.Length;
				}
				vertexBufferStream.Position = 0;
				indexBufferStream.Position = 0;
				VertexBuffer = new Buffer(deviceManager.Device, vertexBufferStream, (int)vertexBufferStream.Length,
					ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, _sizeofVertex);
				IndexBuffer = new Buffer(deviceManager.Device, indexBufferStream, (int)indexBufferStream.Length,
					ResourceUsage.Default, BindFlags.IndexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, _sizeofIndex);
				VertexBufferBinding = new VertexBufferBinding(VertexBuffer, _sizeofVertex, 0);
			}
			Initialized = true;
		}

		public void Uninitialize()
		{
			if(VertexBuffer != null)
			{
				VertexBuffer.Dispose();
				VertexBuffer = null;
			}
			if(IndexBuffer != null)
			{
				IndexBuffer.Dispose();
				IndexBuffer = null;
			}
			VertexBufferBinding = default(VertexBufferBinding);
			_locations.Clear();
			Initialized = false;
		}

		public bool Initialized { get; private set; }

		public bool Contains(string id)
		{
			return _locations.ContainsKey(id);
		}

		public VertexBufferLocation this[string id]
		{
			get
			{
				VertexBufferLocation loc;
				if(!_locations.TryGetValue(id, out loc))
					throw new ArgumentOutOfRangeException("id", "The specified mesh does not exist in this buffer");
				return loc;
			}
		}

		public void Dispose()
		{
			Uninitialize();
		}
	}
}

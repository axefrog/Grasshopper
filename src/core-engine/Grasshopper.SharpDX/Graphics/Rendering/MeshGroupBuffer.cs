using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Primitives;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class MeshGroupBuffer : ActivatableD3DResource, IMeshGroup
	{
		private readonly List<Mesh> _meshes = new List<Mesh>();
		private readonly Dictionary<string, MeshLocation> _locations = new Dictionary<string, MeshLocation>();
		private static readonly int _sizeofVertex = Utilities.SizeOf<Vertex>();
		private static readonly int _sizeofIndex = Utilities.SizeOf<int>();

		public MeshGroupBuffer(DeviceManager deviceManager, string id) : base(deviceManager, id)
		{
		}

		public VertexBufferBinding VertexBufferBinding { get; private set; }
		public Buffer VertexBuffer { get; private set; }
		public Buffer IndexBuffer { get; private set; }

		public int Count { get { return _meshes.Count; } }

		private void AssertIsUninitialized()
		{
			if(IsInitialized)
				throw new InvalidOperationException("This mesh group's resources are currently initialized. Call Uninitialize() first, make changes, then call Initialize() again.");
		}

		public void Add(Mesh mesh)
		{
			AssertIsUninitialized();
			_meshes.Add(mesh);
		}

		public void AddRange(IEnumerable<Mesh> meshes)
		{
			AssertIsUninitialized();
			_meshes.AddRange(meshes);
		}

		public void Remove(Mesh mesh)
		{
			AssertIsUninitialized();
			_meshes.Remove(mesh);
		}

		public IEnumerator<Mesh> GetEnumerator()
		{
			return _meshes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		protected override void InitializeInternal()
		{
			var vertexCount = this.SelectMany(m => m.Vertices).Count();
			var indexCount = this.SelectMany(m => m.Indices).Count();
			using(var vertexBufferStream = new DataStream(_sizeofVertex * vertexCount, true, true))
			using(var indexBufferStream = new DataStream(_sizeofIndex * indexCount, true, true))
			{
				int vbOffset = 0, ibOffset = 0;
				foreach(var mesh in this)
				{
					_locations.Add(mesh.Id, new MeshLocation(mesh.Indices.Length, vbOffset, ibOffset));
					foreach(var vertex in mesh.Vertices)
						vertexBufferStream.Write(vertex);
					foreach(var index in mesh.Indices)
						indexBufferStream.Write(index);
					vbOffset += mesh.Vertices.Length;
					ibOffset += mesh.Indices.Length;
				}
				vertexBufferStream.Position = 0;
				indexBufferStream.Position = 0;
				VertexBuffer = new Buffer(DeviceManager.Device, vertexBufferStream, (int)vertexBufferStream.Length,
					ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, _sizeofVertex);
				IndexBuffer = new Buffer(DeviceManager.Device, indexBufferStream, (int)indexBufferStream.Length,
					ResourceUsage.Default, BindFlags.IndexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, _sizeofIndex);
				VertexBufferBinding = new VertexBufferBinding(VertexBuffer, _sizeofVertex, 0);
			}
		}

		protected override void UninitializeInternal()
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

		protected override void SetActive()
		{
			DeviceManager.Context.InputAssembler.SetVertexBuffers(0, VertexBufferBinding);
			DeviceManager.Context.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
		}

		public void Draw(string id)
		{
			var loc = this[id];
			DeviceManager.Context.DrawIndexed(loc.IndexCount, loc.IndexBufferOffset, loc.VertexBufferOffset);
		}

		public void DrawInstanced(string id, int count)
		{
			var loc = this[id];
			DeviceManager.Context.DrawIndexedInstanced(loc.IndexCount, count, loc.IndexBufferOffset, loc.VertexBufferOffset, 0);
		}
	}
}

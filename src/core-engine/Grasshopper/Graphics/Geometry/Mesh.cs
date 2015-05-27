using System;
using System.Collections.Generic;
using Grasshopper.Assets;
using Grasshopper.Graphics.Geometry.Primitives;

namespace Grasshopper.Graphics.Geometry
{
	public class Mesh : Asset
	{
		public Vertex[] Vertices { get; set; }
		public uint[] Indices { get; set; }

		public Mesh()
		{
		}

		public Mesh(string id)
		{
			((IAsset)this).SetId(id);
		}

		public Mesh Scale(float scale)
		{
			for(int i = 0; i < Vertices.Length; i++)
			{
				var vertex = Vertices[i];
				Vertices[i] = vertex.Scale(scale);
			}
			return this;
		}

		public Mesh FromTriangles(IEnumerable<Triangle> triangles)
		{
			var map = new Dictionary<Vertex, uint>();
			var vertices = new List<Vertex>();
			var indices = new List<uint>();

			foreach(var triangle in triangles)
			{
				foreach(var vertex in triangle)
				{
					uint index;
					if(!map.TryGetValue(vertex, out index))
					{
						index = (uint)vertices.Count;
						map.Add(vertex, index);
						vertices.Add(vertex);
					}
					indices.Add(index);
				}
			}

			Vertices = vertices.ToArray();
			Indices = indices.ToArray();

			return this;
		}
	}

	public abstract class MeshRenderer : IDisposable
	{
		protected MeshRenderer(Mesh mesh)
		{
			Mesh = mesh;
		}

		public Mesh Mesh { get; private set; }
		public bool Initialized { get; private set; }

		/// <summary>
		/// Load data into video memory, ready for use
		/// </summary>
		public void Initialize()
		{
			Uninitialize();
			InitializeResources();
			Initialized = true;
		}

		protected abstract void InitializeResources();

		/// <summary>
		/// Remove data from video memory
		/// </summary>
		public void Uninitialize()
		{
			Initialized = false;
			DestroyResources();
		}

		protected abstract void DestroyResources();

		/// <summary>
		/// Render the mesh
		/// </summary>
		public void Render()
		{
			if(!Initialized) return;
			PerformRender();
		}

		protected abstract void PerformRender();

		protected event Action Disposing;
		public void Dispose()
		{
			var handler = Disposing;
			if(handler != null)
				handler();

			DestroyResources();
		}
	}
}

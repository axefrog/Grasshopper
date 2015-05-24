using System;
using System.Collections.Generic;
using Grasshopper.Assets;
using Grasshopper.Graphics.Geometry.Primitives;

namespace Grasshopper.Graphics.Geometry
{
	public class Mesh : Asset
	{
		public Vertex[] Vertices { get; set; }
		public int[] Indices { get; set; }

		public Mesh()
		{
		}

		public Mesh(string id)
		{
			((IAsset)this).SetId(id);
		}

		public Mesh FromTriangles(IEnumerable<Triangle> triangles)
		{
			var map = new Dictionary<Vertex, int>();
			var vertices = new List<Vertex>();
			var indices = new List<int>();

			foreach(var triangle in triangles)
			{
				foreach(var vertex in triangle)
				{
					int index;
					if(!map.TryGetValue(vertex, out index))
					{
						index = vertices.Count;
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

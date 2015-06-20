using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.Graphics.SceneManagement
{
	public class Mesh<T> where T : struct
	{
		public string Id { get; private set; }
		public T[] Vertices { get; private set; }
		public uint[] Indices { get; private set; }
		public DrawType DrawType { get; private set; }

		public Mesh(string id, T[] vertices, uint[] indices, DrawType drawType)
		{
			Id = id;
			Vertices = vertices;
			Indices = indices;
			DrawType = drawType;
		}
	}

	public class Mesh
	{
		public string Id { get; private set; }
		public VertexPosColTex[] Vertices { get; private set; }
		public uint[] Indices { get; private set; }
		public DrawType DrawType { get; private set; }

		public Mesh(IEnumerable<Triangle> triangles)
			: this(Guid.NewGuid().ToString(), triangles)
		{
		}

		public Mesh(string id, IEnumerable<Triangle> triangles, DrawType drawType = DrawType.Triangles)
		{
			Id = id;
			DrawType = drawType;
			ReadTriangles(triangles);
		}

		public Mesh Scale(float scale)
		{
			for(var i = 0; i < Vertices.Length; i++)
			{
				var vertex = Vertices[i];
				Vertices[i] = vertex.Scale(scale);
			}
			return this;
		}

		private Mesh ReadTriangles(IEnumerable<Triangle> triangles)
		{
			var map = new Dictionary<VertexPosColTex, uint>();
			var vertices = new List<VertexPosColTex>();
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
}

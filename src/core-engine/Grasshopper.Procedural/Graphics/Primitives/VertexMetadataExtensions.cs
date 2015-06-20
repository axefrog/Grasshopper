using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.SceneManagement;

namespace Grasshopper.Procedural.Graphics.Primitives
{
	public static class VertexMetadataExtensions
	{
		public static Mesh<T> ToMesh<T>(this IEnumerable<VertexMetadata> vertexData, string id, Func<VertexMetadata, T> createVertex)
			where T : struct
		{
			var map = new Dictionary<T, uint>();
			uint nextIndex = 0;
			var vertices = new List<T>();
			var indices = new List<uint>();

			foreach(var vertex in vertexData.Select(createVertex))
			{
				uint index;
				if(!map.TryGetValue(vertex, out index))
				{
					map.Add(vertex, index = nextIndex++);
					vertices.Add(vertex);
				}
				indices.Add(index);
			}

			return new Mesh<T>(id, vertices.ToArray(), indices.ToArray(), DrawType.Triangles);
		}
	}
}
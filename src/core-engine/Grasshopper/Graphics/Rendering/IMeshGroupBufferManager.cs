using System;
using Grasshopper.Graphics.Geometry;

namespace Grasshopper.Graphics.Rendering
{
	public interface IMeshGroupBufferManager : IDisposable
	{
		void Add(MeshGroup meshes);
		void Remove(string id);
		void SetActive(string meshGroupId);
		VertexBufferLocation GetMeshLocation(string id);
	}
}
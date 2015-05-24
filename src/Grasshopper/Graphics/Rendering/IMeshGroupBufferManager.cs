using System;
using Grasshopper.Graphics.Geometry;

namespace Grasshopper.Graphics.Rendering
{
	public interface IMeshGroupBufferManager : IDisposable
	{
		void Initialize(MeshGroup meshes);
		void Uninitialize(string id);
		void SetActive(string meshGroupId);
		VertexBufferLocation GetMeshLocation(string id);
	}
}
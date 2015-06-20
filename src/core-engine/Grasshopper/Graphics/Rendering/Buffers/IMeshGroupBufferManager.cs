using System.Collections.Generic;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
//#error do the following:
	// 1. Move SetDrawType to the IDrawingContext interface
	// 2. Move Draw and DrawInstanced to the IDrawingContext interface
	// 3. Now we can lift the mesh group buffer stuff out of the SharpDX layer
	// 5. Consider removing the MeshInstanceBuffer stuff - I think it is exactly replicated by the new vertex buffer stuff and is thus redundant
	// 6. FlappyCraft uses IMeshGroup which is distinct from IMeshGroup<T> - both need to be replaced by #3 (above)

	//-----------------------------------------

	public interface IMesh<T>
		where T : struct
	{
		string Id { get; }
		T[] Vertices { get; }
		uint[] Indices { get; }
		DrawType DrawType { get; }
	}

	public interface IMeshGroup<T> : IActivatablePlatformResource, IEnumerable<IMesh<T>>
		where T : struct
	{
		int Count { get; }
		void Add(IMesh<T> mesh);
		void AddRange(IEnumerable<IMesh<T>> meshes);
		void Remove(IMesh<T> mesh);
		void Draw(string id);
		void DrawInstanced(string id, int count);
	}

	public interface IMeshGroupBufferManager<T> : IActivatablePlatformResourceManager<IMeshGroup<T>>
		where T : struct
	{
		IMeshGroup<T> Create(string id, IEnumerable<IMesh<T>> meshes);
		IMeshGroup<T> Create(string id, params IMesh<T>[] mesh);
	}

	public interface IMeshGroupBufferManagerFactory
	{
		IMeshGroupBufferManager<T> Create<T>()
			where T : struct;
	}
}
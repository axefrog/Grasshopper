using System.Collections.Generic;
using Grasshopper.Graphics.Geometry.Primitives;

namespace Grasshopper.Graphics
{
	/// <summary>
	/// Represents a group of meshes that will be accessed and update in a similar way and which have similar lifespans.
	/// </summary>
	public interface IMeshGroup : IEnumerable<Mesh>
	{
		string Id { get; }
	}

	public interface IMeshBuffer
	{
		string Id { get; }
		IEnumerable<Vertex> Vertices { get; }
		IEnumerable<int> Indices { get; }
	}

	/// <summary>
	/// Indicates parameters for drawing a specific mesh represented by a vertex buffer and corresponding index buffer
	/// </summary>
	public interface IMeshDrawSpec
	{
		string MeshBufferId { get; }
		int IndexCount { get; }
		int IndexOffset { get; }
		DrawType DrawType { get; }
	}

	/// <summary>
	/// Indicates the type of drawing that should occur, given a set of vertices and vertex indices
	/// </summary>
	public enum DrawType
	{
		Points,
		Lines,
		Triangles,
		TriangleStrip
	}
}

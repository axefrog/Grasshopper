namespace Grasshopper.SharpDX.Graphics.Rendering
{
	/// <summary>
	/// Represents location information required to draw a specific mesh stored within a packed vertex buffer
	/// </summary>
	public class MeshLocation
	{
		public MeshLocation(int indexCount, int vertexBufferOffset, int indexBufferOffset)
		{
			IndexCount = indexCount;
			VertexBufferOffset = vertexBufferOffset;
			IndexBufferOffset = indexBufferOffset;
		}

		/// <summary>
		/// The number of indices required to draw the mesh at this location
		/// </summary>
		public int IndexCount { get; private set; }
		/// <summary>
		/// The position of the first vertex representing this mesh in the buffer
		/// </summary>
		public int VertexBufferOffset { get; private set; }
		/// <summary>
		/// The position of the vertex index representing this mesh's triangles in the buffer
		/// </summary>
		public int IndexBufferOffset { get; private set; }
	}
}
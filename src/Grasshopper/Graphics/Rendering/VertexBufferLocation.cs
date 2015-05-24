namespace Grasshopper.Graphics.Rendering
{
	public class VertexBufferLocation
	{
		public VertexBufferLocation(int indexCount, int vertexBufferOffset, int indexBufferOffset)
		{
			IndexCount = indexCount;
			VertexBufferOffset = vertexBufferOffset;
			IndexBufferOffset = indexBufferOffset;
		}

		public int IndexCount { get; private set; }
		public int VertexBufferOffset { get; private set; }
		public int IndexBufferOffset { get; private set; }
	}
}
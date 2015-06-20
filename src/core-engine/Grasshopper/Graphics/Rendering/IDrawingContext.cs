using System;
using Grasshopper.Graphics.Primitives;

namespace Grasshopper.Graphics.Rendering
{
	/// <summary>
	/// Passed as an argument when a render target's Render method is called.
	/// Exposes functionality that is only relevant while a render target is
	/// active for rendering a frame.
	/// </summary>
	public interface IDrawingContext : IDisposable
	{
		void Activate();
		void SetDrawType(DrawType drawType);
		void DrawIndexed(int indexCount, int indexStartLocation, int vertexStartLocation);
		void DrawIndexedInstanced(int indexCountPerInstance, int instanceCount, int indexStartLocation, int vertexStartLocation, int instanceStartLocation);
		void Clear(Color color);
		void Draw(int vertexCount, int vertexStartLocation);
		void DrawInstanced(int vertexCount, int instanceCount, int vertexStartLocation, int instanceStartLocation);
	}
}
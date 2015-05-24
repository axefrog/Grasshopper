using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IRenderContext : IDisposable
	{
		void MakeActive();
		void Exit();
		bool ExitRequested { get; }

		void Clear(Color color);
		void Draw(VertexBufferLocation bufferLocation, DrawType drawType = DrawType.Triangles);
	}
}
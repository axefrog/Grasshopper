using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IRendererContext : IDisposable
	{
		void Clear(Color color);
		void MakeActive();
	}
}
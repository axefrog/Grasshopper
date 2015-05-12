using System;

namespace Grasshopper.Graphics
{
	public interface IRenderer : IDisposable
	{
		IAppWindow Window { get; }
		void Initialize();
		void MakeActive();
		void Clear(Color color);
		void Present();
	}
}
using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IRenderContext : IDisposable
	{
		void Clear(Color color);
		void MakeActive();
		void Exit();
		bool ExitRequested { get; }
	}
}
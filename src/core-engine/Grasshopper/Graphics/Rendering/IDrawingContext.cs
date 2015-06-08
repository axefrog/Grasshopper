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

		void Clear(Color color);
	}
}
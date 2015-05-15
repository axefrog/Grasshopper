using System;

namespace Grasshopper.Graphics
{
	/// <summary>
	/// This is the grand overlord of rendering with a single graphics device. Multiple windows, renderers
	/// and other device-dependent resources are all directly or indirectly managed by this class. Multiple
	/// graphics contexts can exist, but they cannot share resources between each other.
	/// </summary>
	public interface IGraphicsContext : IDisposable
	{
		void Initialize();
		IRendererFactory RendererFactory { get; }
		ITextureLoader TextureLoader { get; }
	}
}
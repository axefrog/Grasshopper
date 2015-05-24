using System;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;

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
		IRenderHostFactory RenderHostFactory { get; }
		ITextureLoader TextureLoader { get; }
		IMaterialManager MaterialManager { get; }
		MeshLibrary MeshLibrary { get; }
		MaterialLibrary MaterialLibrary { get; }
		TextureLibrary TextureLibrary { get; }
	}
}
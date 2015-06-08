using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Blending
{
	public interface IBlendState : IActivatablePlatformResource
	{
		IBlendSettings Settings { get; }
	}
}
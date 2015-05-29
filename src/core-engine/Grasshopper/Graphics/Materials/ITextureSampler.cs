using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
	public interface ITextureSampler : IIndexActivatablePlatformResource
	{
		TextureSamplerSettings Settings { get; }
	}
}
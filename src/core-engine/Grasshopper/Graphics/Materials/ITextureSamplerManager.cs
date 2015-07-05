using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
    public interface ITextureSamplerManager : IIndexActivatablePlatformResourceManager<ITextureSampler>
    {
        ITextureSampler Create(string id, TextureSamplerSettings settings);
    }
}
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Blending
{
    public interface IBlendStateManager : IActivatablePlatformResourceManager<IBlendState>
    {
        IBlendState Create(string id, IBlendSettings settings);
    }
}
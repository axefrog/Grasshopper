using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Rasterization
{
    public interface IRasterizerStateManager : IActivatablePlatformResourceManager<IRasterizerState>
    {
        IRasterizerState Create(string id, IRasterizerSettings settings);
    }
}
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Rasterization
{
    public interface IRasterizerState : IActivatablePlatformResource
    {
        IRasterizerSettings Settings { get; }
    }
}
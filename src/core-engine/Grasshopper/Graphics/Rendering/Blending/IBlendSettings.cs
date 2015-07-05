namespace Grasshopper.Graphics.Rendering.Blending
{
    public interface IBlendSettings
    {
        bool EnableBlend { get; }
        bool EnableAlphaToCoverage { get; }
        PreBlend FirstRGB { get; }
        PreBlend SecondRGB { get; }
        BlendEquation OutputRGB { get; }
        PreBlend FirstAlpha { get; }
        PreBlend SecondAlpha { get; }
        BlendEquation OutputAlpha { get; }
        ColorComponents OutputColorComponents { get; }
        
        BlendSettings Clone();
    }
}
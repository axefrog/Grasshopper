namespace Grasshopper.Graphics.Rendering.Blending
{
    public class BlendSettings : IBlendSettings
    {
        public bool EnableBlend { get; set; }
        public bool EnableAlphaToCoverage { get; set; }
        public PreBlend FirstRGB { get; set; }
        public PreBlend SecondRGB { get; set; }
        public BlendEquation OutputRGB { get; set; }
        public PreBlend FirstAlpha { get; set; }
        public PreBlend SecondAlpha { get; set; }
        public BlendEquation OutputAlpha { get; set; }
        public ColorComponents OutputColorComponents { get; set; }

        public BlendSettings Clone()
        {
            return new BlendSettings
            {
                EnableBlend = EnableBlend,
                EnableAlphaToCoverage = EnableAlphaToCoverage,
                FirstRGB = FirstRGB,
                SecondRGB = SecondRGB,
                OutputRGB = OutputRGB,
                FirstAlpha = FirstAlpha,
                SecondAlpha = SecondAlpha,
                OutputAlpha = OutputAlpha,
                OutputColorComponents = OutputColorComponents,
            };
        }

        public static IBlendSettings None()
        {
            var settings = (BlendSettings)DefaultEnabled();
            settings.EnableBlend = false;
            return settings;
        }

        public static IBlendSettings DefaultEnabled()
        {
            return new BlendSettings
            {
                EnableBlend = true,
                EnableAlphaToCoverage = false,
                FirstRGB = PreBlend.SourceAlpha,
                SecondRGB = PreBlend.InverseSourceAlpha,
                OutputRGB = BlendEquation.AddBothValues,
                FirstAlpha = PreBlend.One,
                SecondAlpha = PreBlend.Zero,
                OutputAlpha = BlendEquation.AddBothValues,
                OutputColorComponents = ColorComponents.All
            };
        }
    }
}
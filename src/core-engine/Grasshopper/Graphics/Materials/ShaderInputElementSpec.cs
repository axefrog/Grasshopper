namespace Grasshopper.Graphics.Materials
{
    public class ShaderInputElementSpec
    {
        public ShaderInputElementFormat Format { get; private set; }
        public ShaderInputElementPurpose Purpose { get; private set; }

        public ShaderInputElementSpec(ShaderInputElementFormat format, ShaderInputElementPurpose purpose)
        {
            Format = format;
            Purpose = purpose;
        }

        public ShaderInputElementSpec(ShaderInputElementFormat format)
        {
            Format = format;
            Purpose = ShaderInputElementPurpose.Custom;
        }
    }
}
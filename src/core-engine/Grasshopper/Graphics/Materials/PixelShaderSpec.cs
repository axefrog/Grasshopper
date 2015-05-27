namespace Grasshopper.Graphics.Materials
{
	public class PixelShaderSpec : ShaderSpec
	{
		public TextureSamplerSettings SamplerSettings { get; private set; }

		public PixelShaderSpec(string source, TextureSamplerSettings samplerSettings = null) : base(source)
		{
			SamplerSettings = samplerSettings ?? TextureSamplerSettings.Default();
		}
	}
}
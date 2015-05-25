namespace Grasshopper.Graphics.Materials
{
	public class TextureSamplerSettings
	{
		private TextureSamplerSettings()
		{
		}

		public TextureSamplerSettings(TextureWrapping wrapU, TextureWrapping wrapV, TextureWrapping wrapW, TextureFiltering filter)
		{
			WrapU = wrapU;
			WrapV = wrapV;
			WrapW = wrapW;
			Filter = filter;
		}

		public TextureWrapping WrapU { get; private set; }
		public TextureWrapping WrapV { get; private set; }
		public TextureWrapping WrapW { get; private set; }
		public TextureFiltering Filter { get; private set; }

		public static TextureSamplerSettings Default()
		{
			return new TextureSamplerSettings
			{
				WrapU = TextureWrapping.Clamp,
				WrapV = TextureWrapping.Clamp,
				WrapW = TextureWrapping.Clamp,
				Filter = TextureFiltering.MinMagMipLinear
			};
		}
	}
}
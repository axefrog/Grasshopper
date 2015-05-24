using System;
using Grasshopper.Assets;

namespace Grasshopper.Graphics.Materials
{
	public class TextureLibrary : AssetGroup<ITexture>
	{
		private readonly ITextureLoader _textureLoader;

		public TextureLibrary(ITextureLoader textureLoader) : base(null)
		{
			_textureLoader = textureLoader;
		}

		public ITexture Load(string id, string path)
		{
			if(AssetExists(id))
				throw new ArgumentException("The specified texture id already exists in the library");
			var texture = _textureLoader.Load(path);
			Add(id, texture);
			return texture;
		}
	}
}
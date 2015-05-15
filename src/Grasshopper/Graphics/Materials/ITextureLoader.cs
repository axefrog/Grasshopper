namespace Grasshopper.Graphics.Materials
{
	public interface ITextureLoader
	{
		ITexture Load(string path);
	}
}
namespace Grasshopper.Graphics.Materials
{
	public class ShaderSpec
	{
		public ShaderSpec(string source)
		{
			Source = source;
		}

		public string Source { get; private set; }
	}
}
namespace Grasshopper.Graphics.Materials
{
	public abstract class ShaderSpec
	{
		protected ShaderSpec(string source)
		{
			Source = source;
		}

		public string Source { get; private set; }
	}
}
namespace Grasshopper.Graphics.Rendering
{
	public interface IWindowRendererContext : IRendererContext
	{
		IAppWindow Window { get; }
		void Present();
	}
}
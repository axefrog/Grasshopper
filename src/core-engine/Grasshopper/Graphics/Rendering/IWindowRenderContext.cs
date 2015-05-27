namespace Grasshopper.Graphics.Rendering
{
	public interface IWindowRenderContext : IRenderContext
	{
		IAppWindow Window { get; }
		void Present();
	}
}
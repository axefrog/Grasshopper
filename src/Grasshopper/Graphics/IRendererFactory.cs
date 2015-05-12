namespace Grasshopper.Graphics
{
	public interface IRendererFactory
	{
		IRenderer Create(IAppWindow window);
		IRenderer CreateWindowed();
	}
}
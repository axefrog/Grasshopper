namespace Grasshopper.Graphics.Rendering
{
	public interface IRendererFactory
	{
		IWindowRenderer CreateWindowed();
	}
}
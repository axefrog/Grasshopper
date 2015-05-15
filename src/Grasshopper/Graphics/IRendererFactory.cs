namespace Grasshopper.Graphics
{
	public interface IRendererFactory
	{
		IWindowRenderer CreateWindowed();
	}
}
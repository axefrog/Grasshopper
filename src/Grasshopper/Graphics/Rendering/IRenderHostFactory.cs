namespace Grasshopper.Graphics.Rendering
{
	public interface IRenderHostFactory
	{
		IWindowRenderHost CreateWindowed();
	}
}
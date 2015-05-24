namespace Grasshopper.Graphics.Rendering
{
	public interface IWindowRenderHost : IRenderHost<IWindowRenderContext>
	{
		IAppWindow Window { get; }
	}
}
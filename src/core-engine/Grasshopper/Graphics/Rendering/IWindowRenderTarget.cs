namespace Grasshopper.Graphics.Rendering
{
	public interface IWindowRenderTarget : IRenderTarget<IWindowDrawingContext>
	{
		IAppWindow Window { get; }
	}
}
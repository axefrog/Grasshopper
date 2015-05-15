namespace Grasshopper.Graphics.Rendering
{
	public interface IWindowRenderer : IRenderer<IWindowRendererContext>
	{
		IAppWindow Window { get; }
	}
}
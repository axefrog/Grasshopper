namespace Grasshopper.Graphics
{
	public interface IGraphicsContextFactory
	{
		IGraphicsContext CreateContext(bool enableDebugMode = false);
	}
}
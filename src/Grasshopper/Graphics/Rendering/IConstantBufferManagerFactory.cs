namespace Grasshopper.Graphics.Rendering
{
	public interface IConstantBufferManagerFactory
	{
		IConstantBufferManager<T> Create<T>() where T : struct;
	}
}
namespace Grasshopper.Graphics.Rendering.Buffers
{
	public interface IConstantBufferManagerFactory
	{
		IConstantBufferManager<T> Create<T>() where T : struct;
	}
}
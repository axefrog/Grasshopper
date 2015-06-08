namespace Grasshopper.Graphics.Rendering.Buffers
{
	public interface IMeshInstanceBufferManagerFactory
	{
		IMeshInstanceBufferManager<T> Create<T>() where T : struct;
	}
}
namespace Grasshopper.Graphics.Rendering
{
	public interface IMeshInstanceBufferManagerFactory
	{
		IMeshInstanceBufferManager<T> Create<T>() where T : struct;
	}
}
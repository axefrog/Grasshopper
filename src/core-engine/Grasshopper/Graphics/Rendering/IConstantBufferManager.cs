using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering
{
	public interface IConstantBufferManager<T> : IIndexActivatablePlatformResourceManager<IConstantBufferResource<T>>
		where T : struct
	{
		IConstantBufferResource<T> Create(string id);
		IConstantBufferResource<T> Create(string id, ref T data);
		void Update(string id, ref T data);
	}
}
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
	public interface IConstantBufferResource<T> : IIndexActivatablePlatformResource
		where T : struct
	{
		void Update(ref T newData);
	}
}
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering
{
	public interface IConstantBufferResource<T> : IIndexActivatablePlatformResource
		where T : struct
	{
		void Update(ref T newData);
	}
}
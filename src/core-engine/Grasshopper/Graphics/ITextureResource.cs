using Grasshopper.Platform;

namespace Grasshopper.Graphics
{
	public interface ITextureResource : IIndexActivatablePlatformResource
	{
		void SetFileSource(IFileSource fileSource);
	}
}
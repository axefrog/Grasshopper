using Grasshopper.Platform;

namespace Grasshopper.Graphics.Materials
{
	public interface ITextureResource : IIndexActivatablePlatformResource
	{
		void SetFileSource(IFileSource fileSource);
	}
}
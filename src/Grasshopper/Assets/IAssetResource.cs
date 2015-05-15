using System.IO;

namespace Grasshopper.Assets
{
	public interface IAssetResource
	{
		Stream OpenRead();
		string Path { get; }
		int Size { get; }
	}
}
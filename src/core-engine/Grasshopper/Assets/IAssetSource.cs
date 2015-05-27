using System.IO;

namespace Grasshopper.Assets
{
	public interface IAssetSource
	{
		Stream OpenRead();
		string Path { get; }
		int Size { get; }
	}
}
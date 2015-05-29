using System.IO;

namespace Grasshopper.Platform
{
	public interface IFileSource
	{
		Stream OpenRead();
		string Path { get; }
		int Size { get; }
	}
}
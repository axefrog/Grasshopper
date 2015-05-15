using System.IO;

namespace Grasshopper.Assets
{
	// todo: Evolve this into something inspired by, and as capable as, PhysFS
	public interface IAssetReader
	{
		Stream OpenRead(string path);
	}
}

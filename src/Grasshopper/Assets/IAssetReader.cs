using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasshopper.Assets
{
	// todo: Evolve this into something inspired by, and as capable as, PhysFS
	public interface IAssetReader
	{
		Stream OpenRead(string path);
	}
}

using System;
using Grasshopper.Assets;

namespace Grasshopper.Graphics.Materials
{
	public interface ITexture : IDisposable
	{
		IAssetResource Asset { get; }
	}
}
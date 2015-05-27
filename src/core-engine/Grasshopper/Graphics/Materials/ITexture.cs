using System;
using Grasshopper.Assets;

namespace Grasshopper.Graphics.Materials
{
	public interface ITexture : IAsset, IDisposable
	{
		IAssetSource AssetSource { get; }
	}
}
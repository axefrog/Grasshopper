using System;
using Grasshopper.Assets;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics
{
	public class Texture : ITexture
	{
		public IAssetSource AssetSource { get; private set; }
		public ShaderResourceView ShaderResourceView { get; private set; }

		public Texture(IAssetSource assetSource, ShaderResourceView shaderResourceView)
		{
			AssetSource = assetSource;
			ShaderResourceView = shaderResourceView;
		}

		public void Dispose()
		{
			ShaderResourceView.Dispose();
			ShaderResourceView = null;
			AssetSource = null;
		}

		private string _id;
		string IAsset.Id { get { return _id; } }

		void IAsset.SetId(string id)
		{
			if(_id != null)
				throw new InvalidOperationException("Id is immutable and cannot be changed");
			_id = id;
		}
	}
}
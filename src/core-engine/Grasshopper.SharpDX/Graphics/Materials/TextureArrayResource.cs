using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	class TextureArrayResource : D3DTextureResource
	{
		private TextureResource[] _textures;
		private Texture2D _textureArray;

		public TextureArrayResource(DeviceManager deviceManager, string id) : base(deviceManager, id)
		{
		}

		public void SetTextures(IEnumerable<TextureResource> sourceTextures)
		{
			_textures = sourceTextures.ToArray();
		}

		protected override void InitializeInternal()
		{
			var sourceTextures = _textures.Select(t =>
			{
				var view = t.ShaderResourceView;
				if(view == null)
					throw new InvalidOperationException(string.Format("Texture array cannot be created because source texture '{0}' is not initialized", t.Id));
				return view.Resource.QueryInterface<Texture2D>();
			}).ToArray();

			var descr = sourceTextures[0].Description;
			descr.ArraySize = _textures.Length;
			_textureArray = new Texture2D(DeviceManager.Device, descr);
			ShaderResourceView = new ShaderResourceView(DeviceManager.Device, _textureArray);

			var mipLevels = descr.MipLevels;
			for(var i = 0; i < mipLevels; i++)
			{
				for(var j = 0; j < _textures.Length; j++)
				{
					var texture = sourceTextures[j];
					DeviceManager.Context.CopySubresourceRegion(texture, i, null, _textureArray, mipLevels * j + i);
				}
			}
		}

		protected override void UninitializeInternal()
		{
			if(ShaderResourceView != null)
			{
				ShaderResourceView.Dispose();
				ShaderResourceView = null;
			}
			if(_textureArray != null)
			{
				_textureArray.Dispose();
				_textureArray = null;
			}
		}

		protected override void ActivateAtIndex(int index)
		{
			DeviceManager.Context.PixelShader.SetShaderResource(index, ShaderResourceView);
		}
	}
}
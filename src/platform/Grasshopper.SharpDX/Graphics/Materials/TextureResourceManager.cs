using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Platform;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	class TextureResourceManager : IndexActivatablePlatformResourceManager<ITextureResource>, ITextureResourceManager
	{
		private readonly DeviceManager _deviceManager;
		private readonly Lazy<IFileStore> _fileStore;

		public TextureResourceManager(DeviceManager deviceManager, Lazy<IFileStore> fileStore)
		{
			_deviceManager = deviceManager;
			_fileStore = fileStore;
		}

		protected override ITextureResource CreateResource(string id)
		{
			return new TextureResource(_deviceManager, id);
		}

		protected override void Activate(int firstIndex, IEnumerable<ITextureResource> resources)
		{
			_deviceManager.Context.PixelShader.SetShaderResources(firstIndex, resources.Select((r, i) =>
			{
				var texture = (D3DTextureResource)r;
				texture.SetActivatedExternally(firstIndex + i);
				return texture.ShaderResourceView;
			}).ToArray());
		}

		public ITextureResource Create(string id, string path)
		{
			var assetSource = _fileStore.Value.GetFile(path);
			return Create(id, assetSource);
		}

		public ITextureResource Create(string id, IFileSource fileSource)
		{
			return CreateAndAdd(id, resource =>
			{
				var texture = (TextureResource)resource;
				texture.SetFileSource(fileSource);
				texture.Initialize();
			});
		}

		public ITextureResource CreateArray(string textureArrayId, params string[] sourceTextureIds)
		{
			var textureArrayResource = (TextureArrayResource)CreateAndAttachEventHandlers(textureArrayId, resid => new TextureArrayResource(_deviceManager, resid));
			var textures = sourceTextureIds.Select(id =>
			{
				var sourceTexture = this[id] as TextureResource;
				if(sourceTexture == null)
					throw new InvalidOperationException(string.Format("Texture resource '{0}' cannot be used in a texture array as it is of type {1}", textureArrayId, this[id].GetType().FullName));
				return sourceTexture;
			});
			textureArrayResource.SetTextures(textures);
			textureArrayResource.Initialize();
			AddAndNotifySubscribers(textureArrayResource);
			return textureArrayResource;
		}
	}
}

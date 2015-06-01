using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Platform;

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
				var texture = (TextureResource)r;
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
			return CreateAndAdd(id, texture =>
			{
				texture.SetFileSource(fileSource);
				texture.Initialize();
			});
		}
	}
}

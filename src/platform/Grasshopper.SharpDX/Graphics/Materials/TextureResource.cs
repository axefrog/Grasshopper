using Grasshopper.Platform;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	class TextureResource : D3DTextureResource
	{
		private IFileSource _fileSource;

		public TextureResource(DeviceManager deviceManager, string id) : base(deviceManager, id)
		{
		}

		public void SetFileSource(IFileSource fileSource)
		{
			_fileSource = fileSource;
		}

		protected override void InitializeInternal()
		{
			// todo: make sure the texture has actually loaded successfully - note that it may not be loading from a regular file, so we can't rely on FileNotFoundException bubbling up; wrap it in a local exception
			using(var stream = _fileSource.OpenRead())
				ShaderResourceView = ShaderResourceView.FromStream(DeviceManager.Device, stream, _fileSource.Size);
		}

		protected override void UninitializeInternal()
		{
			if(ShaderResourceView != null)
			{
				ShaderResourceView.Dispose();
				ShaderResourceView = null;
			}
		}

		protected override void ActivateAtIndex(int index)
		{
			DeviceManager.Context.PixelShader.SetShaderResource(index, ShaderResourceView);
		}
	}
}
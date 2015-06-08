using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	internal abstract class D3DTextureResource : IndexActivatableD3DResource, ITextureResource
	{
		protected D3DTextureResource(DeviceManager deviceManager, string id) : base(deviceManager, id)
		{
		}

		public ShaderResourceView ShaderResourceView { get; protected set; }
	}
}
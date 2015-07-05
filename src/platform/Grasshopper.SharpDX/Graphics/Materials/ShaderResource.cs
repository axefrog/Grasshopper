using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
    internal abstract class ShaderResource : IndexActivatableD3DResource
    {
        protected ShaderResource(DeviceManager deviceManager, string id) : base(deviceManager, id)
        {
        }

        public ShaderResourceView ShaderResourceView { get; protected set; }

        protected override void ActivateAtIndex(int index)
        {
            DeviceManager.Context.PixelShader.SetShaderResource(index, ShaderResourceView);
        }
    }
}
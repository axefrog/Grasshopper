using System;
using Grasshopper.Graphics.Rendering.Rasterization;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    class RasterizerState : ActivatableD3DResource, IRasterizerState
    {
        public RasterizerState(DeviceManager deviceManager, string id, IRasterizerSettings settings) : base(deviceManager, id)
        {
            if(settings == null) throw new ArgumentNullException("settings");
            Settings = settings;
        }

        public global::SharpDX.Direct3D11.RasterizerState State { get; private set; }
        public IRasterizerSettings Settings { get; private set; }

        protected override void InitializeInternal()
        {
            var desc = RasterizerStateDescription.Default();
            desc.IsFrontCounterClockwise = Settings.WindingOrder == WindingOrder.Counterclockwise;
            desc.FillMode = Settings.RenderWireframe ? FillMode.Wireframe : FillMode.Solid;
            desc.IsDepthClipEnabled = Settings.EnableDepthTest;
            desc.IsMultisampleEnabled = Settings.Antialiasing == Antialiasing.Multisample;
            desc.IsAntialiasedLineEnabled = Settings.Antialiasing == Antialiasing.LinesOnly;
            switch(Settings.TriangleCulling)
            {
                case TriangleCulling.DrawFrontFacing:
                    desc.CullMode = CullMode.Back;
                    break;
                case TriangleCulling.DrawBackFacing:
                    desc.CullMode = CullMode.Front;
                    break;
                case TriangleCulling.DrawAll:
                    desc.CullMode = CullMode.None;
                    break;
            }
            State = new global::SharpDX.Direct3D11.RasterizerState(DeviceManager.Device, desc);
        }

        protected override void UninitializeInternal()
        {
            if(State != null)
            {
                State.Dispose();
                State = null;
            }
        }

        protected override void SetActive()
        {
            DeviceManager.Context.Rasterizer.State = State;
        }
    }
}

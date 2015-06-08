using System;
using Grasshopper.Graphics.Rendering.Rasterization;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	class RasterizerState : ActivatableD3DResource, IRasterizerState
	{
		private readonly DeviceManager _deviceManager;

		public RasterizerState(DeviceManager deviceManager, string id) : base(deviceManager, id)
		{
			_deviceManager = deviceManager;
		}

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
			State = new global::SharpDX.Direct3D11.RasterizerState(_deviceManager.Device, desc);
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
			_deviceManager.Context.Rasterizer.State = State;
		}

		public global::SharpDX.Direct3D11.RasterizerState State { get; private set; }
		public IRasterizerSettings Settings { get; internal set; }
	}
}

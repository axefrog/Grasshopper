using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Rendering;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using Color = Grasshopper.Graphics.Color;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public abstract class RenderContext : IRenderContext
	{
		private readonly DeviceManager _deviceManager;
		private RenderTargetView _renderTargetView;
		private DepthStencilView _depthStencilView;
		private readonly Dictionary<RasterizerSettings, RasterizerState> _rasterizerStates = new Dictionary<RasterizerSettings, RasterizerState>();

		protected RenderContext(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
			RasterizerSettings = RasterizerSettings.Default();
			SetDrawType(DrawType.Triangles);
		}

		public RasterizerSettings RasterizerSettings { get; set; }

		protected void SetRenderTargetView(RenderTargetView renderTargetView)
		{
			_renderTargetView = renderTargetView;
		}

		protected void SetDepthStencilView(DepthStencilView depthStencilView)
		{
			_depthStencilView = depthStencilView;
		}

		public void MakeActive()
		{
			if(_renderTargetView == null)
				throw new InvalidOperationException("The implementation of this class forgot to call SetRenderTargetView during initialization");
			var dc = _deviceManager.Context;

			dc.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);
			UpdateRasterizerState();
		}

		private void UpdateRasterizerState()
		{
			if(!RasterizerSettings.IsDirty) return;

			RasterizerState state;
			if(!_rasterizerStates.TryGetValue(RasterizerSettings, out state))
			{
				var desc = RasterizerStateDescription.Default();
				desc.IsFrontCounterClockwise = RasterizerSettings.WindingOrder == WindingOrder.Counterclockwise;
				desc.FillMode = RasterizerSettings.RenderWireframe ? FillMode.Wireframe : FillMode.Solid;
				desc.IsDepthClipEnabled = RasterizerSettings.EnableDepthTest;
				desc.IsMultisampleEnabled = RasterizerSettings.Antialiasing == Antialiasing.Multisample;
				desc.IsAntialiasedLineEnabled = RasterizerSettings.Antialiasing == Antialiasing.LinesOnly;
				switch(RasterizerSettings.TriangleCulling)
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
				state = new RasterizerState(_deviceManager.Device, desc);
				_rasterizerStates.Add(RasterizerSettings, state);
			}
			_deviceManager.Context.Rasterizer.State = state;

			RasterizerSettings.IsDirty = false;
		}

		public void Exit()
		{
			ExitRequested = true;
		}

		public bool ExitRequested { get; private set; }

		public void Clear(Color color)
		{
			var dc = _deviceManager.Context;
			dc.ClearRenderTargetView(_renderTargetView, new Color4(color.ToRgba()));
			dc.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1f, 0);
		}

		public void SetDrawType(DrawType drawType)
		{
			switch(drawType)
			{
				case DrawType.Points:
					_deviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.PointList;
					break;
				case DrawType.LineList:
					_deviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
					break;
				case DrawType.LineStrip:
					_deviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineStrip;
					break;
				case DrawType.Triangles:
					_deviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
					break;
				case DrawType.TriangleStrip:
					_deviceManager.Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
					break;
				default:
					throw new NotSupportedException("Unexpected draw type: " + drawType);
			}
		}

		public void Draw(VertexBufferLocation loc)
		{
			_deviceManager.Context.DrawIndexed(loc.IndexCount, loc.IndexBufferOffset, loc.VertexBufferOffset);
		}

		public void DrawInstances(VertexBufferLocation loc, int instanceCount)
		{
			_deviceManager.Context.DrawIndexedInstanced(loc.IndexCount, instanceCount, loc.IndexBufferOffset, loc.VertexBufferOffset, 0);
		}

		protected event Action Disposing;
		protected bool IsDisposed { get; private set; }

		public void Dispose()
		{
			var handler = Disposing;
			if(handler != null)
				handler();

			foreach(var rasterizer in _rasterizerStates.Values)
				rasterizer.Dispose();
			_rasterizerStates.Clear();

			IsDisposed = true;
		}
	}
}
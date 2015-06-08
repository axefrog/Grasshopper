using Grasshopper.Graphics.Rendering;
using Color = Grasshopper.Graphics.Primitives.Color;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
	public abstract class DrawingContext : IDrawingContext
	{
		protected DeviceManager DeviceManager { get; private set; }

		protected DrawingContext(DeviceManager deviceManager)
		{
			DeviceManager = deviceManager;
		}

		public void Activate()
		{
			MakeTargetsActive();
		}

		protected abstract void MakeTargetsActive();
		public abstract void Clear(Color color);

		protected bool IsDisposed { get; private set; }
		protected abstract void DestroyResources();

		public void Dispose()
		{
			DestroyResources();
			IsDisposed = true;
		}
	}
}
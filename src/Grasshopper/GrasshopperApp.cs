using System;
using System.Threading;
using Grasshopper.Assets;
using Grasshopper.Graphics;

namespace Grasshopper
{
	public class GrasshopperApp : IDisposable
	{
		public IAssetReader AssetReader { get; set; }
		public IGraphicsContextFactory GraphicsContextFactory { get; set; }

		public void Dispose()
		{
		}

		public void Run(IRenderer renderer, RenderFrameHandler main)
		{
			using(var ev = new AutoResetEvent(false))
				while(renderer.Render(main))
					ev.WaitOne(1);
		}

		public void Run(Func<bool> main)
		{
			using(var ev = new AutoResetEvent(false))
				while(main())
					ev.WaitOne(1);
		}
	}
}

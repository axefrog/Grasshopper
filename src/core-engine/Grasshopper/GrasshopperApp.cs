using System;
using System.Threading;
using Grasshopper.Core;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;
using Grasshopper.Platform;

namespace Grasshopper
{
	public class GrasshopperApp : IDisposable
	{
		private DateTime _startTime;

		public GrasshopperApp()
		{
		}

		public IFileStore Files { get; set; }
		public IGraphicsContextFactory Graphics { get; set; }
		public IInputContext Input { get; set; }
		public TickCounter TickCounter { get; private set; }
		public TimeSpan Elapsed { get { return DateTime.UtcNow - _startTime; } }
		public float ElapsedSeconds { get { return (float)(DateTime.UtcNow - _startTime).TotalSeconds; } }

		public void Run<TRendererContext>(IRenderHost<TRendererContext> renderHost, RenderFrameHandler<TRendererContext> main)
			where TRendererContext : IRenderContext
		{
			_startTime = DateTime.UtcNow;
			using(var ev = new AutoResetEvent(false))
				while(!renderHost.ExitRequested)
				{
					renderHost.Render(main);
					ev.WaitOne(1);
				}
		}

		public virtual void Run(Func<bool> main)
		{
			_startTime = DateTime.UtcNow;
			TickCounter = new TickCounter();
			while(main())
				TickCounter.Tick();
		}

		public virtual void Dispose()
		{
		}
	}
}

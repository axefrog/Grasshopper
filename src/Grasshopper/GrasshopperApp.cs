using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Grasshopper.Assets;
using Grasshopper.Core;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Rendering;
using Grasshopper.StateManagement;

namespace Grasshopper
{
	public class GrasshopperApp : IDisposable
	{
		private readonly Subject<IGameEvent> _gameEvents;
		private DateTime _startTime;

		public GrasshopperApp()
		{
			_gameEvents = new Subject<IGameEvent>();
			GameEvents = _gameEvents.AsObservable();
		}

		public IAssetStore Assets { get; set; }
		public IGraphicsContextFactory Graphics { get; set; }
		public TickCounter TickCounter { get; private set; }
		public IObservable<IGameEvent> GameEvents { get; private set; }
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

		public void Run(Func<bool> main)
		{
			_startTime = DateTime.UtcNow;
			TickCounter = new TickCounter();
			while(main())
				TickCounter.Tick();
		}

		public void PostGameEvent(IGameEvent gameEvent)
		{
			_gameEvents.OnNext(gameEvent);
		}

		public void Dispose()
		{
		}
	}
}

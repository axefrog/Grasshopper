using System;
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
        private bool _exiting;
        private FrameContext _frameContext;

        public GrasshopperApp()
        {
        }

        public IFileStore Files { get; set; }
        public IGraphicsContextFactory Graphics { get; set; }
        public IInputContext Input { get; set; }
        public TickCounter TickCounter { get; private set; }
        public TimeSpan Elapsed { get { return DateTime.UtcNow - _startTime; } }
        public float ElapsedSeconds { get { return (float)Elapsed.TotalSeconds; } }

        private void Initialize()
        {
            TickCounter = new TickCounter();
            _frameContext = new FrameContext(this);
            _startTime = DateTime.UtcNow;
        }

        private void NextFrame()
        {
            TickCounter.Tick();
            _frameContext.NextFrame();
        }

        public void Run<TRendererContext>(IRenderTarget<TRendererContext> renderTarget, RenderFrameHandlerEx<TRendererContext> main)
            where TRendererContext : IDrawingContext
        {
            Initialize();
            while(!renderTarget.Terminated && !_exiting)
            {
                renderTarget.Render(_frameContext, main);
                NextFrame();
            }
        }

        public virtual void Run(MainLoopFrameHandler main)
        {
            Initialize();
            while(main(_frameContext) && !_exiting)
                NextFrame();
        }

        public void Exit()
        {
            _exiting = true;
        }

        public virtual void Dispose()
        {
        }
    }

    public delegate bool MainLoopFrameHandler(FrameContext frame);
}

using System;
using Grasshopper.Graphics.Rendering;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    public class RenderTarget<T> : IRenderTarget<T>
        where T : IDrawingContext
    {
        private T _targetContext;

        public bool Terminated { get; protected set; }

        public RenderTarget(T drawingContext)
        {
            _targetContext = drawingContext;
        }

        public virtual void Render(FrameContext frame, RenderFrameHandlerEx<T> renderFrame)
        {
            if(Terminated) return;
            _targetContext.Activate();
            renderFrame(frame, _targetContext);
        }

        public virtual void Render(RenderFrameHandler<T> renderFrame)
        {
            if(Terminated) return;
            _targetContext.Activate();
            renderFrame(_targetContext);
        }

        protected event Action Disposing;

        public void Dispose()
        {
            var handler = Disposing;
            if(handler != null)
                handler();

            _targetContext.Dispose();
            _targetContext = default(T);
        }
    }
}

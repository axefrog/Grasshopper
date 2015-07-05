using System;

namespace Grasshopper.Graphics.Rendering
{
    public interface IRenderTarget<T> : IDisposable
        where T : IDrawingContext
    {
        void Render(FrameContext frame, RenderFrameHandlerEx<T> renderFrame);
        void Render(RenderFrameHandler<T> renderFrame);
        bool Terminated { get; }
    }

    public delegate void RenderFrameHandlerEx<T>(FrameContext frame, T context)
        where T : IDrawingContext;

    public delegate void RenderFrameHandler<T>(T context)
        where T : IDrawingContext;
}
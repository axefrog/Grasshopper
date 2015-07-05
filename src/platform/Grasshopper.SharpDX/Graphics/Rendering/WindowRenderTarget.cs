using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    class WindowRenderTarget : RenderTarget<IWindowDrawingContext>, IWindowRenderTarget
    {
        private readonly WindowDrawingContext _drawingContext;

        public WindowRenderTarget(GraphicsContext graphics, IInputContext input) : this(new WindowDrawingContext(graphics, input))
        {
        }

        private WindowRenderTarget(WindowDrawingContext drawingContext) : base(drawingContext)
        {
            _drawingContext = drawingContext;
        }

        public IAppWindow Window { get { return _drawingContext.Window; } }

        public override void Render(FrameContext frame, RenderFrameHandlerEx<IWindowDrawingContext> renderFrame)
        {
            if(Window != null && !Window.NextFrame())
            {
                Terminated = true;
                return;
            }

            base.Render(frame, renderFrame);
        }

        public void Initialize()
        {
            _drawingContext.Initialize();
        }
    }
}
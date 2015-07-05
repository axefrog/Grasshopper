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

        private bool CheckWindowStatus()
        {
            if(Window != null && !Window.NextFrame())
            {
                Terminated = true;
                return false;
            }
            return true;
        }

        public override void Render(FrameContext frame, RenderFrameHandlerEx<IWindowDrawingContext> renderFrame)
        {
            if(!CheckWindowStatus()) return;
            base.Render(frame, renderFrame);
        }

        public override void Render(RenderFrameHandler<IWindowDrawingContext> renderFrame)
        {
            if(!CheckWindowStatus()) return;
            base.Render(renderFrame);
        }

        public void Initialize()
        {
            _drawingContext.Initialize();
        }
    }
}
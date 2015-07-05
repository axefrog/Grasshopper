namespace Grasshopper.Graphics.Rendering
{
    public interface IWindowDrawingContext : IDrawingContext
    {
        IGraphicsContext Graphics { get; }
        IAppWindow Window { get; }
        void Present();
    }
}
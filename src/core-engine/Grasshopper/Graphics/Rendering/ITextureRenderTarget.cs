namespace Grasshopper.Graphics.Rendering
{
    public interface ITextureRenderTarget : IRenderTarget<ITextureDrawingContext>
    {
        void ActivateTextureResource(int index);
    }
}
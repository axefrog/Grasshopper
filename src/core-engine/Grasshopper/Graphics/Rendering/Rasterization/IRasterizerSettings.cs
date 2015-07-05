namespace Grasshopper.Graphics.Rendering.Rasterization
{
    public interface IRasterizerSettings
    {
        WindingOrder WindingOrder { get; }
        bool RenderWireframe { get; }
        bool EnableDepthTest { get; }
        TriangleCulling TriangleCulling { get; }
        Antialiasing Antialiasing { get; }
        RasterizerSettings Clone();
    }
}
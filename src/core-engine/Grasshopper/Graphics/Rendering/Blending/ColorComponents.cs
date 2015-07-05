namespace Grasshopper.Graphics.Rendering.Blending
{
    public enum ColorComponents
    {
        Red = 1,
        Green = 2,
        Blue = 4,
        Alpha = 8,
        All = Red | Green | Blue | Alpha
    }
}
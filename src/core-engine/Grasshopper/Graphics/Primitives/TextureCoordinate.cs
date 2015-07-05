using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Primitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TextureCoordinate
    {
        public float U { get; set; }
        public float V { get; set; }

        public static TextureCoordinate From(float u, float v)
        {
            return new TextureCoordinate { U = u, V = v };
        }
    }
}
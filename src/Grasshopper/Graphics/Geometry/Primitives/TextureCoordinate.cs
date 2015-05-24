using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Geometry.Primitives
{
	[StructLayout(LayoutKind.Sequential)]
	public struct TextureCoordinate
	{
		float U { get; set; }
		float V { get; set; }

		public static TextureCoordinate From(float u, float v)
		{
			return new TextureCoordinate { U = u, V = v };
		}
	}
}
using System.Linq;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry;
using Grasshopper.Graphics.Geometry.Primitives;

namespace Grasshopper.Procedural.Graphics.Primitives
{
	public static class Cube
	{
		public static Mesh Unit()
		{
			return Unit(Color.White);
		}

		public static Mesh Unit(Color color)
		{
			return Unit(color, color, color, color, color, color, color, color);
		}

		public static Mesh Unit(Color a, Color b, Color c, Color d, Color e, Color f, Color g, Color h)
		{
			var front = Quad.SquareXY(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f).SetColors(a, b, c, d);
			var back = Quad.SquareXY(0.5f, -0.5f, -0.5f, 0.5f, 0.5f).SetColors(e, f, g, h);
			var left = Quad.SquareYZ(-0.5f, 0.5f, 0.5f, -0.5f, -0.5f).SetColors(f, a, d, g);
			var right = Quad.SquareYZ(-0.5f, 0.5f, -0.5f, 0.5f, 0.5f).SetColors(b, e, h, c);
			var top = Quad.SquareXZ(-0.5f, 0.5f, 0.5f, -0.5f, 0.5f).SetColors(f, e, b, a);
			var bottom = Quad.SquareXZ(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f).SetColors(a, b, e, f);
			
			return new Mesh().FromTriangles(new[] { front, back, left, right, top, bottom }.SelectMany(m => m));
		}
	}
}

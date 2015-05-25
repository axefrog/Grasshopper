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
			var front = Quad.XY(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f, a, b, c, d);
			var back = Quad.XY(0.5f, -0.5f, -0.5f, 0.5f, 0.5f, e, f, g, h);
			var left = Quad.YZ(-0.5f, 0.5f, 0.5f, -0.5f, -0.5f, f, a, d, g);
			var right = Quad.YZ(-0.5f, 0.5f, -0.5f, 0.5f, 0.5f, b, e, h, c);
			var top = Quad.XZ(-0.5f, 0.5f, 0.5f, -0.5f, 0.5f, f, e, b, a);
			var bottom = Quad.XZ(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f, a, b, e, f);
			
			return new Mesh().FromTriangles(new[] { front, back, left, right, top, bottom }.SelectMany(m => m));
		}
	}
}

using System.Linq;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Geometry.Primitives;

namespace Grasshopper.Procedural.Graphics.Primitives
{
	public static class Cube
	{
		public static Mesh Unit()
		{
			var front = Quad.SquareXY(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f);
			var back = Quad.SquareXY(0.5f, -0.5f, -0.5f, 0.5f, 0.5f);
			var left = Quad.SquareYZ(-0.5f, 0.5f, 0.5f, -0.5f, -0.5f);
			var right = Quad.SquareYZ(-0.5f, 0.5f, -0.5f, 0.5f, 0.5f);
			var top = Quad.SquareXZ(-0.5f, 0.5f, 0.5f, -0.5f, 0.5f);
			var bottom = Quad.SquareXZ(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f);
			
			return new Mesh().FromTriangles(new[] { front, back, left, right, top, bottom }.SelectMany(m => m));
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.SceneManagement;

namespace Grasshopper.Procedural.Graphics.Primitives
{
	public static class Cube
	{
		public static Mesh Unit(string id)
		{
			return Unit(id, Color.White);
		}

		public static Mesh Unit(string id, Color color)
		{
			return Unit(id, color, color, color, color, color, color, color, color);
		}

		public static Mesh Unit(string id, Color a, Color b, Color c, Color d, Color e, Color f, Color g, Color h)
		{
			var front  = Quad.XY(-0.5f,  0.5f, -0.5f,  0.5f,  0.5f, a, b, c, d);
			var back   = Quad.XY( 0.5f, -0.5f, -0.5f,  0.5f, -0.5f, e, f, g, h);
			var left   = Quad.YZ(-0.5f,  0.5f, -0.5f,  0.5f, -0.5f, f, a, d, g);
			var right  = Quad.YZ(-0.5f,  0.5f,  0.5f, -0.5f,  0.5f, b, e, h, c);
			var top    = Quad.XZ(-0.5f,  0.5f,  0.5f, -0.5f,  0.5f, f, e, b, a);
			var bottom = Quad.XZ(-0.5f,  0.5f, -0.5f,  0.5f, -0.5f, d, c, e, f);

			return new Mesh(id, new[] { front, back, left, right, top, bottom }.SelectMany(m => m));
		}

		public static IEnumerable<VertexMetadata> Generate()
		{
			yield return new VertexMetadata() { Color = Color.Red, FaceIndex = 0, Position = new Vector4()};
		}
	}
}

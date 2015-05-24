using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Geometry.Primitives
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Quad : IEnumerable<Triangle>
	{
		public Triangle A { get; set; }
		public Triangle B { get; set; }

		public static Quad From(Vertex a, Vertex b, Vertex c, Vertex d)
		{
			return new Quad
			{
				A = Triangle.From(a, b, c),
				B = Triangle.From(a, c, d)
			};
		}

		public static Quad Homogeneous()
		{
			return SquareXY(-1.0f, 1.0f, -1.0f, 1.0f);
		}

		public static Quad SquareXY(float x0 = -0.5f, float x1 = 0.5f, float y0 = -0.5f, float y1 = 0.5f, float zPosition = 0.0f)
		{
			return From(
				Vertex.From(x0, y1, zPosition, 0.0f, 0.0f),
				Vertex.From(x1, y1, zPosition, 1.0f, 0.0f),
				Vertex.From(x1, y0, zPosition, 1.0f, 1.0f),
				Vertex.From(x0, y0, zPosition, 0.0f, 1.0f));
		}

		public static Quad SquareXZ(float x0 = -0.5f, float x1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float yPosition = 0.0f)
		{
			return From(
				Vertex.From(x0, z1, yPosition, 0.0f, 0.0f),
				Vertex.From(x1, z1, yPosition, 1.0f, 0.0f),
				Vertex.From(x1, z0, yPosition, 1.0f, 1.0f),
				Vertex.From(x0, z0, yPosition, 0.0f, 1.0f));
		}

		public static Quad SquareYZ(float y0 = -0.5f, float y1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float xPosition = 0.0f)
		{
			return From(
				Vertex.From(y1, z1, xPosition, 0.0f, 0.0f),
				Vertex.From(y1, z0, xPosition, 1.0f, 0.0f),
				Vertex.From(y0, z0, xPosition, 1.0f, 1.0f),
				Vertex.From(y0, z1, xPosition, 0.0f, 1.0f));
		}

		public IEnumerator<Triangle> GetEnumerator()
		{
			yield return A;
			yield return B;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
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

		public Mesh ToMesh(string id)
		{
			return new Mesh(id, this);
		}

		public static Quad From(Vertex a, Vertex b, Vertex c, Vertex d)
		{
			return new Quad
			{
				A = Triangle.From(a, b, c),
				B = Triangle.From(a, c, d)
			};
		}

		public static Quad Homogeneous(Color color1 = default(Color), Color color2 = default(Color), Color color3 = default(Color), Color color4 = default(Color))
		{
			return XY(-1.0f, 1.0f, -1.0f, 1.0f, 0.0f, color1, color2, color3, color4);
		}

		public static Quad XY(float x0 = -0.5f, float x1 = 0.5f, float y0 = -0.5f, float y1 = 0.5f, float zPosition = 0.0f, Color color1 = default(Color), Color color2 = default(Color), Color color3 = default(Color), Color color4 = default(Color))
		{
			return From(
				Vertex.From(x0, y1, zPosition, 0.0f, 0.0f, color1),
				Vertex.From(x1, y1, zPosition, 1.0f, 0.0f, color2),
				Vertex.From(x1, y0, zPosition, 1.0f, 1.0f, color3),
				Vertex.From(x0, y0, zPosition, 0.0f, 1.0f, color4));
		}

		public static Quad XZ(float x0 = -0.5f, float x1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float yPosition = 0.0f, Color color1 = default(Color), Color color2 = default(Color), Color color3 = default(Color), Color color4 = default(Color))
		{
			return From(
				Vertex.From(x0, yPosition, z1, 0.0f, 0.0f, color1),
				Vertex.From(x1, yPosition, z1, 1.0f, 0.0f, color2),
				Vertex.From(x1, yPosition, z0, 1.0f, 1.0f, color3),
				Vertex.From(x0, yPosition, z0, 0.0f, 1.0f, color4));
		}

		public static Quad YZ(float y0 = -0.5f, float y1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float xPosition = 0.0f, Color color1 = default(Color), Color color2 = default(Color), Color color3 = default(Color), Color color4 = default(Color))
		{
			return From(
				Vertex.From(xPosition, y1, z0, 0.0f, 0.0f, color1),
				Vertex.From(xPosition, y1, z1, 1.0f, 0.0f, color2),
				Vertex.From(xPosition, y0, z1, 1.0f, 1.0f, color3),
				Vertex.From(xPosition, y0, z0, 0.0f, 1.0f, color4));
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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Grasshopper.Graphics.SceneManagement;

namespace Grasshopper.Graphics.Primitives
{
	public struct Quad<T> : IEnumerable<Triangle<T>>
		where T : struct
	{
		public Triangle<T> A { get; set; }
		public Triangle<T> B { get; set; }

		public IEnumerator<Triangle<T>> GetEnumerator()
		{
			yield return A;
			yield return B;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Quad : IEnumerable<Triangle>
	{
		public Triangle A { get; set; }
		public Triangle B { get; set; }

		public Mesh ToMesh(string id)
		{
			return new Mesh(id, this);
		}

		public static Quad From(VertexPosColTex a, VertexPosColTex b, VertexPosColTex c, VertexPosColTex d)
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
				VertexPosColTex.From(x0, y1, zPosition, 0.0f, 0.0f, color1),
				VertexPosColTex.From(x1, y1, zPosition, 1.0f, 0.0f, color2),
				VertexPosColTex.From(x1, y0, zPosition, 1.0f, 1.0f, color3),
				VertexPosColTex.From(x0, y0, zPosition, 0.0f, 1.0f, color4));
		}

		public static Quad XZ(float x0 = -0.5f, float x1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float yPosition = 0.0f, Color color1 = default(Color), Color color2 = default(Color), Color color3 = default(Color), Color color4 = default(Color))
		{
			return From(
				VertexPosColTex.From(x0, yPosition, z1, 0.0f, 0.0f, color1),
				VertexPosColTex.From(x1, yPosition, z1, 1.0f, 0.0f, color2),
				VertexPosColTex.From(x1, yPosition, z0, 1.0f, 1.0f, color3),
				VertexPosColTex.From(x0, yPosition, z0, 0.0f, 1.0f, color4));
		}

		public static Quad YZ(float y0 = -0.5f, float y1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float xPosition = 0.0f, Color color1 = default(Color), Color color2 = default(Color), Color color3 = default(Color), Color color4 = default(Color))
		{
			return From(
				VertexPosColTex.From(xPosition, y1, z0, 0.0f, 0.0f, color1),
				VertexPosColTex.From(xPosition, y1, z1, 1.0f, 0.0f, color2),
				VertexPosColTex.From(xPosition, y0, z1, 1.0f, 1.0f, color3),
				VertexPosColTex.From(xPosition, y0, z0, 0.0f, 1.0f, color4));
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
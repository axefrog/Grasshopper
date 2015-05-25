using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Geometry.Primitives
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Triangle : IEnumerable<Vertex>
	{
		public readonly Vertex A;
		public readonly Vertex B;
		public readonly Vertex C;

		public Triangle(Vertex a, Vertex b, Vertex c)
		{
			A = a;
			B = b;
			C = c;
		}

		public static Triangle From(Vertex a, Vertex b, Vertex c)
		{
			return new Triangle(a, b, c);
		}

		public IEnumerator<Vertex> GetEnumerator()
		{
			yield return A;
			yield return B;
			yield return C;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
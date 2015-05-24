using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Geometry.Primitives
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Triangle : IEnumerable<Vertex>
	{
		public Vertex A { get; set; }
		public Vertex B { get; set; }
		public Vertex C { get; set; }

		public static Triangle From(Vertex a, Vertex b, Vertex c)
		{
			return new Triangle { A = a, B = b, C = c };
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
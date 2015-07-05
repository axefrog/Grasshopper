using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Primitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle<T> where T : struct
    {
        public readonly T A;
        public readonly T B;
        public readonly T C;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle : IEnumerable<VertexPosColTex>
    {
        public readonly VertexPosColTex A;
        public readonly VertexPosColTex B;
        public readonly VertexPosColTex C;

        public Triangle(VertexPosColTex a, VertexPosColTex b, VertexPosColTex c)
        {
            A = a;
            B = b;
            C = c;
        }

        public static Triangle From(VertexPosColTex a, VertexPosColTex b, VertexPosColTex c)
        {
            return new Triangle(a, b, c);
        }

        public IEnumerator<VertexPosColTex> GetEnumerator()
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
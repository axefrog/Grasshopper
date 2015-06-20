using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Grasshopper.Graphics.Primitives;

namespace Grasshopper.Procedural.Graphics.Primitives
{
	public class TriangleBuilder : IEnumerable<VertexMetadata>
	{
		private int _vertexCount;
		private readonly Vector3[] _xyz = new Vector3[3];
		private readonly Vector2[] _uv = new Vector2[3];
		private Color[] _colors;
		private uint _faceIndex;
		private uint _triangleIndex;

		public static TriangleBuilder New { get { return new TriangleBuilder(); } }

		public TriangleBuilder Colors(params Color[] colors)
		{
			if(colors.Length != 3)
				throw new ArgumentException("Triangle builder requires exactly 3 colors to be specified. You specified " + colors.Length + ".");
			_colors = colors;
			return this;
		}

		public TriangleBuilder Vertex(Vector3 pos, Vector2 uv)
		{
			if(_vertexCount == 3) _vertexCount = 0;
			_xyz[_vertexCount] = pos;
			_uv[_vertexCount++] = uv;
			return this;
		}

		public TriangleBuilder Vertex(float x, float y, float z, float u, float v)
		{
			if(_vertexCount == 3) _vertexCount = 0;
			_xyz[_vertexCount] = new Vector3(x, y, z);
			_uv[_vertexCount++] = new Vector2(u, v);
			return this;
		}

		public TriangleBuilder FaceIndex(uint faceIndex)
		{
			_faceIndex = faceIndex;
			return this;
		}

		public TriangleBuilder TriangleIndex(uint triangleIndex)
		{
			_triangleIndex = triangleIndex;
			return this;
		}

		public IEnumerator<VertexMetadata> GetEnumerator()
		{
			if(_colors == null) Colors();
			var colorIndex = 0;
			for(var i = 0; i < 3; i++)
			{
				yield return new VertexMetadata(_xyz[i].X, _xyz[i].Y, _xyz[i].Z, _uv[i].X, _uv[i].Y, _colors[colorIndex], _faceIndex, _triangleIndex);
				if(++colorIndex >= _colors.Length) colorIndex = 0;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
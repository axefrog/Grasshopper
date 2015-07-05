using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Grasshopper.Graphics.Primitives;

namespace Grasshopper.Procedural.Graphics.Primitives
{
    public class QuadBuilder : IEnumerable<VertexMetadata>
    {
        private Color[] _colors;
        private Vector3[] _xyz;
        private Vector2[] _uv;
        private uint _faceIndex;

        public static QuadBuilder New { get { return new QuadBuilder(); } }

        public QuadBuilder XY(float x0 = -0.5f, float x1 = 0.5f, float y0 = -0.5f, float y1 = 0.5f, float z = 0.0f)
        {
            _xyz = new []
            {
                new Vector3(x0, y1, z),
                new Vector3(x1, y1, z),
                new Vector3(x1, y0, z),
                new Vector3(x0, y0, z)
            };
            return this;
        }

        public QuadBuilder XZ(float x0 = -0.5f, float x1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float y = 0.0f)
        {
            _xyz = new[]
            {
                new Vector3(x0, y, z1),
                new Vector3(x1, y, z1),
                new Vector3(x1, y, z0),
                new Vector3(x0, y, z0)
            };
            return this;
        }

        public QuadBuilder YZ(float y0 = -0.5f, float y1 = 0.5f, float z0 = -0.5f, float z1 = 0.5f, float x = 0.0f)
        {
            _xyz = new[]
            {
                new Vector3(x, y1, z0),
                new Vector3(x, y1, z1),
                new Vector3(x, y0, z1),
                new Vector3(x, y0, z0)
            };
            return this;
        }

        public QuadBuilder UV(float u0 = 0.0f, float u1 = 1.0f, float v0 = 0.0f, float v1 = 1.0f)
        {
            _uv = new[]
            {
                new Vector2(u0, v0),
                new Vector2(u1, v0),
                new Vector2(u1, v1),
                new Vector2(u0, v1),
            };
            return this;
        }

        /// <summary>
        /// Initializes a set of quad vertices positioned so that the quad fills the bounds of homogeneous clip space (-1.0f -> 1.0f on the x and y axes)
        /// </summary>
        /// <param name="z">Optionally sets the z coordinate for each vertex</param>
        /// <returns>An instance of the current quad builder</returns>
        public QuadBuilder Homogeneous(float z = 0)
        {
            return XY(-1.0f, 1.0f, -1.0f, 1.0f, z);
        }

        public QuadBuilder Colors(params Color[] colors)
        {
            if(colors.Length != 4)
                throw new ArgumentException("Quad builder requires exactly 4 colors to be specified. You specified " + colors.Length + ".");
            _colors = colors;
            return this;
        }

        public QuadBuilder Face(uint index)
        {
            _faceIndex = index;
            return this;
        }

        public IEnumerator<VertexMetadata> GetEnumerator()
        {
            if(_colors == null) Colors();
            if(_xyz == null) XY();
            if(_uv == null) UV();

            var t1 = TriangleBuilder.New
                .FaceIndex(_faceIndex)
                .TriangleIndex(_faceIndex * 2)
                .Colors(_colors[0], _colors[1], _colors[2])
                .Vertex(_xyz[0], _uv[0])
                .Vertex(_xyz[1], _uv[1])
                .Vertex(_xyz[2], _uv[2]);
            
            var t2 = TriangleBuilder.New
                .FaceIndex(_faceIndex)
                .TriangleIndex(_faceIndex * 2 + 1)
                .Colors(_colors[0], _colors[2], _colors[3])
                .Vertex(_xyz[0], _uv[0])
                .Vertex(_xyz[2], _uv[2])
                .Vertex(_xyz[3], _uv[3]);

            return t1.Concat(t2).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
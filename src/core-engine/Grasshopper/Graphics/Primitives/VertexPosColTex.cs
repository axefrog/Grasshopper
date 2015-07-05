using System.Numerics;
using System.Runtime.InteropServices;
using Grasshopper.Graphics.Materials;

namespace Grasshopper.Graphics.Primitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosColTex
    {
        public readonly Vector4 Position;
        public readonly Color4 Color;
        public readonly TextureCoordinate TextureCoordinate;

        public static readonly ShaderInputElementSpec[] ShaderInputLayout =
        {
            ShaderInputElementPurpose.Position.CreateSpec(),
            ShaderInputElementPurpose.Color.CreateSpec(),
            ShaderInputElementPurpose.TextureCoordinate.CreateSpec(),
            new ShaderInputElementSpec(ShaderInputElementFormat.Float2, ShaderInputElementPurpose.Padding),
        };

        public VertexPosColTex(Vector4 position, Color color, TextureCoordinate textureCoordinate)
            : this(position, color.ToColor4(), textureCoordinate)
        {
        }

        public VertexPosColTex(Vector4 position, Color4 color, TextureCoordinate textureCoordinate)
        {
            Position = position;
            Color = color;
            TextureCoordinate = textureCoordinate;
        }

        public VertexPosColTex Scale(float scale)
        {
            return new VertexPosColTex(Position*scale, Color, TextureCoordinate);
        }

        public static VertexPosColTex From(Vector4 pos)
        {
            return From(pos, Primitives.Color.White);
        }

        public static VertexPosColTex From(Vector4 pos, TextureCoordinate coord)
        {
            return From(pos, Primitives.Color.White, coord);
        }

        public static VertexPosColTex From(Vector4 pos, Color color)
        {
            return From(pos, color, TextureCoordinate.From(0.0f, 0.0f));
        }

        public static VertexPosColTex From(Vector4 pos, Color color, TextureCoordinate coord)
        {
            return new VertexPosColTex(pos, color, coord);
        }

        public static VertexPosColTex From(float x, float y, float z, float u, float v, Color color = default(Color))
        {
            return From(new Vector4(x, y, z, 1.0f), color, TextureCoordinate.From(u, v));
        }

        public bool Equals(VertexPosColTex other)
        {
            return Position.Equals(other.Position) && TextureCoordinate.Equals(other.TextureCoordinate);
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is VertexPosColTex && Equals((VertexPosColTex)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position.GetHashCode();
                hashCode = (hashCode*397) ^ TextureCoordinate.GetHashCode();
                return hashCode;
            }
        }
    }
}
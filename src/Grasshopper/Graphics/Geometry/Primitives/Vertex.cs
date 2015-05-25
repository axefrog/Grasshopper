using System.Numerics;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Geometry.Primitives
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vertex
	{
		public readonly Vector4 Position;
		public readonly Color4 Color;
		public readonly TextureCoordinate TextureCoordinate;

		public Vertex(Vector4 position, Color color, TextureCoordinate textureCoordinate)
		{
			Position = position;
			Color = color.ToColor4();
			TextureCoordinate = textureCoordinate;
		}

		public static Vertex From(Vector4 pos)
		{
			return From(pos, Graphics.Color.White);
		}

		public static Vertex From(Vector4 pos, TextureCoordinate coord)
		{
			return From(pos, Graphics.Color.White, coord);
		}

		public static Vertex From(Vector4 pos, Color color)
		{
			return From(pos, color, TextureCoordinate.From(0.0f, 0.0f));
		}

		public static Vertex From(Vector4 pos, Color color, TextureCoordinate coord)
		{
			return new Vertex(pos, color, coord);
		}

		public static Vertex From(float x, float y, float z, float u, float v, Color color = default(Color))
		{
			return From(new Vector4(x, y, z, 1.0f), color, TextureCoordinate.From(u, v));
		}

		public bool Equals(Vertex other)
		{
			return Position.Equals(other.Position) && TextureCoordinate.Equals(other.TextureCoordinate);
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj)) return false;
			return obj is Vertex && Equals((Vertex)obj);
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
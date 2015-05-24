using System.Numerics;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Geometry.Primitives
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vertex
	{
		public Vector4 Position;
		public Color Color;
		public TextureCoordinate TextureCoordinate;

		public void SetColor(Color color)
		{
			Color = color;
		}

		public static Vertex From(Vector4 pos)
		{
			return From(pos, Color.White);
		}

		public static Vertex From(Vector4 pos, TextureCoordinate coord)
		{
			return From(pos, Color.White, coord);
		}

		public static Vertex From(Vector4 pos, Color color)
		{
			return From(pos, color, TextureCoordinate.From(0.0f, 0.0f));
		}

		public static Vertex From(Vector4 pos, Color color, TextureCoordinate coord)
		{
			return new Vertex
			{
				Position = pos,
				Color = color,
				TextureCoordinate = coord,
			};
		}

		public static Vertex From(float x, float y, float z, float u, float v)
		{
			return From(new Vector4(x, y, z, 0.0f), TextureCoordinate.From(u, v));
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
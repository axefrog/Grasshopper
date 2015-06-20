// Type: Grasshopper.Graphics.Color4
// Assembly: SharpDX, Version=2.5.0.0, Culture=neutral, PublicKeyToken=627a3d6d1956f55a
// MVID: FA03B5B6-823F-48BF-8919-09A7E490B380
// Assembly location: D:\Dropbox\Work\Sandbox\Game Dev\SharpDx4.1\packages\SharpDX.2.5.0\lib\net40\SharpDX.dll

using System;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics.Primitives
{
	/// <summary>
	/// Represents a color in the form of rgba.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Color4 : IEquatable<Color4>, IFormattable
	{
		/// <summary>
		/// The red component of the color.
		/// </summary>
		public float Red;
		/// <summary>
		/// The green component of the color.
		/// </summary>
		public float Green;
		/// <summary>
		/// The blue component of the color.
		/// </summary>
		public float Blue;
		/// <summary>
		/// The alpha component of the color.
		/// </summary>
		public float Alpha;

		public static implicit operator Color4(Color color)
		{
			return color.ToColor4();
		}

		public bool Equals(Color4 other)
		{
			return Red.Equals(other.Red) && Green.Equals(other.Green) && Blue.Equals(other.Blue) && Alpha.Equals(other.Alpha);
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj)) return false;
			return obj is Color4 && Equals((Color4)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
// ReSharper disable NonReadonlyFieldInGetHashCode
				var hashCode = Red.GetHashCode();
				hashCode = (hashCode * 397) ^ Green.GetHashCode();
				hashCode = (hashCode * 397) ^ Blue.GetHashCode();
				hashCode = (hashCode * 397) ^ Alpha.GetHashCode();
// ReSharper restore NonReadonlyFieldInGetHashCode
				return hashCode;
			}
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			throw new NotImplementedException();
		}
	}
}

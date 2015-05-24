using System;

namespace Grasshopper.Graphics.Materials
{
	public enum ShaderInputElementPurpose
	{
		Position,
		Color,
		TextureCoordinate,
		Normal,
		Custom
	}

	public static class ShaderInputElementPurposeExtensions
	{
		public static ShaderInputElementSpec Spec(this ShaderInputElementPurpose purpose)
		{
			switch(purpose)
			{
				case ShaderInputElementPurpose.Position:
					return new ShaderInputElementSpec(ShaderInputElementFormat.Float4, purpose);
				case ShaderInputElementPurpose.Normal:
					return new ShaderInputElementSpec(ShaderInputElementFormat.Float4, purpose);
				case ShaderInputElementPurpose.TextureCoordinate:
					return new ShaderInputElementSpec(ShaderInputElementFormat.Float2, purpose);
				case ShaderInputElementPurpose.Color:
					return new ShaderInputElementSpec(ShaderInputElementFormat.Float4, purpose);
				default:
					throw new NotSupportedException("Cannot automatically generate input element spec from this value");
			}
		}
	}
}
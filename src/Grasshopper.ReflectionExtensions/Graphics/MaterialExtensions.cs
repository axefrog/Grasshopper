namespace Grasshopper.Graphics
{
	public static class MaterialExtensions
	{
		//private static readonly Dictionary<Type, ShaderInputElementSpec> TypeMappings = new Dictionary<Type, ShaderInputElementSpec>()
		//{
		//	{ typeof(int), new ShaderInputElementSpec(ShaderInputElementFormat.Int32, ShaderInputElementPurpose.Custom) },
		//	{ typeof(float), new ShaderInputElementSpec(ShaderInputElementFormat.Float, ShaderInputElementPurpose.Custom) },
		//	{ typeof(Vertex), new ShaderInputElementSpec(ShaderInputElementFormat.Float4, ShaderInputElementPurpose.Position) },
		//};
		//public static ShaderInputElementSpec[] CreateInputElementSpecs<TInstance>(this Material material)
		//	where TInstance : struct
		//{
		//	typeof(TInstance)
		//		.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
		//		.Select(field =>
		//		{
		//			var type = field.FieldType;
		//			ShaderInputElementFormat format;
		//			ShaderInputElementPurpose purpose;
					
		//			if(type == typeof(int))
		//			{
		//				format = ShaderInputElementFormat.Int32;
		//				purpose = ShaderInputElementPurpose.Custom;
		//			}
		//			var spec = new ShaderInputElementSpec()
		//		});
		//}
	}
}

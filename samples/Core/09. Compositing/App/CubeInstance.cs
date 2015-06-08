using System.Numerics;
using System.Runtime.InteropServices;

namespace CompositingSample.App
{
	[StructLayout(LayoutKind.Sequential)]
	struct CubeInstance
	{
		public Vector4 Position;
		public Vector4 Rotation;
		public Vector4 Scale;
		public int Texture;
		public Vector3 _pad0;
	}
}
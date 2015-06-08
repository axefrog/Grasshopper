using System.Numerics;
using System.Runtime.InteropServices;

namespace CompositingSample.App
{
	[StructLayout(LayoutKind.Sequential)]
	struct SceneData
	{
		public Matrix4x4 View;
		public Matrix4x4 Projection;
		public float SecondsElapsed;
		private readonly Vector3 _pad0;
	}
}
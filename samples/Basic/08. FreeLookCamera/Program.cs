using System.Numerics;
using FreeLookCamera.App;
using Grasshopper.SharpDX;
using Grasshopper.WindowsFileSystem;

namespace FreeLookCamera
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new CubesDemo(50000)
				.UseSharpDX()
				.UseWindowsFileSystem())
			{
				app.InitializeResources();
				app.Run(() =>
				{
					app.Camera.Position = new Vector3(0, 0, app.ElapsedSeconds*10);
					return true;
				});
			}
		}
	}
}

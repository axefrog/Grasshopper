using CompositingSample.App;
using Grasshopper;
using Grasshopper.SharpDX;
using Grasshopper.WindowsFileSystem;

namespace CompositingSample
{
	class CompositingApp : GrasshopperApp
	{
		// 1. render the scene to a texture
		// 2. render the rear-vision scene to another texture (same scene, different camera)
		// 3. render a scene with nothing but a rear vision mirror, textured with #2, above
		// 4. render a flat scene containing five animals textures
		// 5. for the final window, draw a single quad three times; first with the main scene, second with the rear vision mirror, third with the animal squares

		static void Main(string[] args)
		{
			// We're gonna refactor the existing cubes demo to ensure it's simple and easy to follow.
			// The core mesh buffer should be initialized here, not in a separate class.
			// todo: CubesDemo should become CubesScene and expose a drawing method which takes a camera argument
			// todo: Create a RearVisionMirrorScene class which contains a tilted quad in which the rearvision scene texture can be rendered
			// todo: Create a AnimalPanelsScene class which is like the other two scenes, but flat with five animal quads
			// todo: Each of the above scenes (x3) exposes a texture resource that can be activated. Do step #5 above.
			// todo: Optional: try and introduce a "TV lines" effect into the rear vision mirror

			using(var app = new CubesDemo(totalCubes: 100000)
				.UseSharpDX()
				.UseWindowsFileSystem())
			{
				app.InitializeResources();

				var input = new CubesInput(app);

				app.Run(() =>
				{
					input.Apply();
					if(input.UserRequestedExit)
						return false;

					return true;
				});
			}
		}
	}
}

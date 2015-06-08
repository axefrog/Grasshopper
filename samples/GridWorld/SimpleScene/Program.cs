using Grasshopper.Graphics;
using Grasshopper.GridWorld;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;
using Grasshopper.WindowsFileSystem;

namespace SimpleScene
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GridWorldApp()
				.UseSharpDX()
				.UseWindowsFileSystem())

			using(var gfx = app.Graphics.CreateContext())
			using(var renderer = gfx.RenderTargetFactory.CreateWindow())
			{
				//renderer.Window.Visible = true;
				//renderer.Window.Title = "Grasshopper Samples / GridWorld / Simple Scene";

				//var textures = new TextureLibrary(gfx.TextureManager);
				//var materials = new MaterialLibrary(textures);
				//var meshes = new MeshLibrary();
				//var models = new ModelLibrary();
				//var blocks = new BlockLibrary();

				//textures.Load("tiles/rabbit", "Textures/rabbit.jpg");
				//textures.Load("tiles/bricks", "Textures/bricks.jpg");
				//textures.Load("tiles/drymud", "Textures/drymud.jpg");
				//textures.Load("tiles/grass", "Textures/grass.jpg");
				//textures.Load("tiles/water", "Textures/water.jpg");

				//materials.Add("rabbit").WithTexture("tiles/rabbit");
				//materials.Add("bricks").WithTexture("tiles/bricks");
				//materials.Add("drymud").WithTexture("tiles/drymud");
				//materials.Add("grass" ).WithTexture("tiles/grass");
				//materials.Add("water" ).WithTexture("tiles/water");

				//meshes.Add("cube", Cube.Unit("cube"));
				
				//models.Add("cubes/rabbit").WithMesh("cube").WithMaterial("rabbit");
				//models.Add("cubes/bricks").WithMesh("cube").WithMaterial("bricks");
				//models.Add("cubes/drymud").WithMesh("cube").WithMaterial("drymud");
				//models.Add("cubes/grass").WithMesh("cube").WithMaterial("grass");
				//models.Add("cubes/water").WithMesh("cube").WithMaterial("water");

				//blocks.Add("solid/rabbit").WithModel("cubes/rabbit");

				////var ecs = new EntityComponentSystem();
				////var cube = ecs.CreateEntity();
				

				

				////blocks.Add("solid/rabbit", new BlockTemplate());

				
				////var rabbitTexture = gfx.TextureLoader.Load("Textures/rabbit.jpg");
				////material.Textures.Add(rabbitTexture);

				////var rabbitBlockTemplate = new BlockTemplate("rabbitblock");
				////var model = new Model("rabbitblock", new ModelMesh("rabbit", Cube.Unit()));
				////rabbitBlockTemplate.Meshes.Add(Cube.Unit());

				////var zone = new WorldZone(10, 10, 1);
				////var loc = new GridLocation();
				////loc.TexturePlanes.Add(TexturedPlane.CreateFloor(1.0f, "rabbit"));
				////zone.Fill(loc);

				
				//// what do we want to do here?
				//// define a few map locations
				//// each location needs a few texture planes
				//	// texture planes seems a bit finicky... can we encapsulate this better?
				//	// each plane within the cube should be positioned as x/y/z with values in the range [0-1]
				//// need to point a camera at the origin

				//app.Run(renderer, context =>
				//{
				//	context.Clear(Color.Black);
				//	context.Present();
				//});
			}
		}
	}

	//public static class CubeEntity
	//{
	//	public static Entity CreateCubeEntity(this EntityComponentSystem ecs)
	//	{
	//		var entity = ecs.CreateEntity();
	//		var state = new State();
	//		entity.AddComponent(state);
	//	}

	//	public class State
	//	{
	//		//public Transformation
	//	}
	//}
}

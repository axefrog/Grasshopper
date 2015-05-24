using Artemis;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;

namespace SimpleCube
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GrasshopperApp().UseSharpDX())
			using(var gfx = app.Graphics.CreateContext())
			using(var renderer = gfx.RenderHostFactory.CreateWindowed())
			{
				renderer.Window.Title = "Simple Cube";
				renderer.Window.ShowBordersAndTitle = true;
				renderer.Window.Visible = true;
				renderer.Window.Resizable = true;

				app.Run(renderer, context =>
				{
					context.Clear(Color.CornflowerBlue);
					context.Present();

					var cubeMesh = Cube.Unit();
					gfx.MeshLibrary.Add("cube", Cube.Unit());
					gfx.MaterialLibrary.Add("white", new MaterialSpec());

					var instance = new MeshInstance("cube", "white");
					var world = new EntityWorld();
					var cube = world.CreateEntity();
					//cube.AddComponent<>();

					return true;
				});
			}
		}


	}
}

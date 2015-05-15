using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.GridWorld;
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

			using(var gfx = app.Graphics.Create())
			using(var renderer = gfx.RendererFactory.CreateWindowed())
			{
				renderer.Window.Visible = true;
				renderer.Window.Title = "Grasshopper Samples / GridWorld / Simple Scene";

				var materials = new MaterialLibrary();
				var material = new Material();
				var texture = gfx.TextureLoader.Load("Textures/rabbit.jpg");
				material.Textures.Add(texture);

				app.Run(renderer, context =>
				{
					context.Clear(Color.Black);
					context.Present();
					return true;
				});
			}
		}
	}
}

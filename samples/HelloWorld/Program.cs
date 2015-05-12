using System;
using Grasshopper.Graphics;
using Grasshopper.SharpDX;

namespace HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = SharpDXBootstrapper.CreateGrasshopperApp())
			using(var main = app.Services.Renderers.CreateWindowed())
			using(var other = app.Services.Renderers.CreateWindowed())
			{
				app.Run(() =>
				{
					main.Window.Visible = true;
					other.Window.Visible = true;

					return main.Next(context =>
					{
						context.Window.Title = "Hello, window #1! It's currently " + DateTime.UtcNow.ToString("F");
						context.Clear(Color.CornflowerBlue);
						context.Present();
						return true;

					}) && other.Next(context =>
					{
						context.Window.Title = "Hello, window #2! It's currently " + DateTime.UtcNow.ToString("F");
						context.Clear(Color.Tomato);
						context.Present();
						return true;
					});
				});
			}
		}
	}
}

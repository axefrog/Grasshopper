using System;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.SharpDX;

namespace HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GrasshopperApp().UseSharpDX())
			using(var gfx = app.GraphicsContextFactory.Create())
			using(var main = gfx.RendererFactory.CreateWindowed())
			using(var other = gfx.RendererFactory.CreateWindowed())
			{
				main.Window.Visible = true;
				other.Window.Visible = true;

				app.Run(() =>
				{
					return main.Render(context =>
					{
						context.Window.Title = "Hello, window #1! It's currently " + DateTime.UtcNow.ToString("F");
						context.Clear(Color.CornflowerBlue);
						context.Present();
						return true;
					})
					&& other.Render(context =>
					{
						context.Window.Title = "Hello, window #2! It's currently " + DateTime.UtcNow.ToString("F");
						context.Clear(Color.Tomato);
						context.Present();
						return true;
					})
					;
				});
			}
		}
	}
}

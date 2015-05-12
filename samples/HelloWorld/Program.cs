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

					return main.Window.NextFrame(win =>
					{
						win.Title = "Hello, window #1! It's currently " + DateTime.UtcNow.ToString("F");
						main.Clear(Color.CornflowerBlue);
						main.Present();
						return true;

					}) && other.Window.NextFrame(win =>
					{
						win.Title = "Hello, window #2! It's currently " + DateTime.UtcNow.ToString("F");
						other.Clear(Color.Tomato);
						other.Present();
						return true;
					});
				});
			}
		}
	}
}

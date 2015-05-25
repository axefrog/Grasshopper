using System;
using Grasshopper;
using Grasshopper.Core;
using Grasshopper.Graphics;
using Grasshopper.SharpDX;

namespace HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = new GrasshopperApp().UseSharpDX())
			using(var gfx = app.Graphics.CreateContext(enableDebugMode: true))
			using(var main = gfx.RenderHostFactory.CreateWindowed())
			using(var other = gfx.RenderHostFactory.CreateWindowed())
			{
				main.Window.Visible = true;
				other.Window.Visible = true;

				var fpsLimiter = new RateLimiter(60);

				app.Run(() =>
				{
					if(!fpsLimiter.Ready())
						return true;

					main.Render(context =>
					{
						context.Window.Title = "Hello, window #1! Current ticks per second: " + app.TickCounter.TicksPerSecond.ToString("0");
						context.Clear(Color.CornflowerBlue);
						context.Present();
					});
					
					other.Render(context =>
					{
						context.Window.Title = "Hello, window #2! It's currently " + DateTime.UtcNow.ToString("F");
						context.Clear(Color.Tomato);
						context.Present();
					});

					return !(main.ExitRequested && other.ExitRequested);
				});
			}
		}
	}
}

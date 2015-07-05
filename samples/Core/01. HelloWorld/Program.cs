using System;
using Grasshopper;
using Grasshopper.Core;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Primitives;
using Grasshopper.SharpDX;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var app = new GrasshopperApp().UseSharpDX())
            using(var gfx = app.Graphics.CreateContext(enableDebugMode: true))

            // In this sample we'll create two windows and display each of them with different colours and
            // titles displaying different information
            using(var main = gfx.RenderTargetFactory.CreateWindow())
            using(var other = gfx.RenderTargetFactory.CreateWindow())
            {
                main.Window.Visible = true;
                other.Window.Visible = true;

                // Our main loop runs as fast as possible, but we only want to render at 60fps
                var fpsLimiter = new RateLimiter(60);

                app.Run(frame =>
                {
                    if(!fpsLimiter.Ready())
                        return true;

                    // The first window will show our true frame rate which should be over a million cycles
                    // per second (of which only 60 per second will include the redraws below)
                    main.Render(context =>
                    {
                        context.Window.Title = "Hello, window #1! Current ticks per second: " + frame.FramesPerSecond.ToString("0");
                        context.Clear(Color.CornflowerBlue);
                        context.Present();
                    });

                    // The second window will show the current time
                    other.Render(context =>
                    {
                        context.Window.Title = "Hello, window #2! It's currently " + DateTime.UtcNow.ToString("F");
                        context.Clear(Color.Tomato);
                        context.Present();
                    });

                    // Closing a window sends an exit request, which we can treat as we see fit. In this
                    // case, we will terminate the application by returning false from the main loop only
                    // when both windows are closed.
                    return !(main.Terminated && other.Terminated);
                });
            }
        }
    }
}

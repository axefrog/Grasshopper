using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;
using SimpleQuad.Properties;

namespace SimpleQuad
{
	class Program : IDisposable
	{
		static void Main()
		{
			using(var program = new Program())
			{
				program.Run();
			}
		}

		private readonly GrasshopperApp _app;
		private readonly IGraphicsContext _graphics;
		private readonly IWindowRenderTarget _renderTarget;
		private readonly IVertexBufferManager<Vertex> _vertexBufferManager;

		public Program()
		{
			_app = new GrasshopperApp().UseSharpDX();
			_graphics = _app.Graphics.CreateContext(enableDebugMode: true);
			_renderTarget = _graphics.RenderTargetFactory.CreateWindow();
			_vertexBufferManager = _graphics.VertexBufferManagerFactory.Create<Vertex>();

			_renderTarget.Window.Title = "Simple Quad";
			_renderTarget.Window.Visible = true;
		}

		private void Run()
		{
			// Prepare our default material which will simply render
			// out using the vertex colour. We then set the material
			// active, which sets the active shaders in GPU memory,
			// ready for drawing with.
			var material = _graphics.MaterialManager.Create("simple");
			material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, Vertex.ShaderInputLayout);
			material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
			material.Activate();

			// We'll use Grasshopper's procedural tools to quickly
			// build a set of vertices for our quad
			var vertices = QuadBuilder.New
				.XY()
				.Colors(Color.Red, Color.Green, Color.Blue, Color.Yellow)
				.Select(v => new Vertex(v.Position, v.Color))
				.ToArray();

			// Create a vertex buffer. We don't need to dispose of it
			// ourselves; it'll automatically get cleaned up when the
			// vertex manager that created it is disposed of.
			var vertexBuffer = _vertexBufferManager.Create("quad");

			// Six vertices will be written here; 3 per triangle
			using(var writer = vertexBuffer.BeginWrite(vertices.Length))
				writer.Write(vertices);

			// Normal vertex buffers should be activated in slot 0; we
			// only use other slots when doing more advanced things
			// with shaders.
			vertexBuffer.Activate(0);

			// Now that our material and vertex buffers are activated
			// and ready to go, let's initiate our main loop and draw
			// our quad!
			_app.Run(_renderTarget, context =>
			{
				context.Clear(Color.CornflowerBlue);
				context.SetDrawType(DrawType.Triangles);
				context.Draw(vertices.Length, 0);
				context.Present();
			});
		}

		public void Dispose()
		{
			_vertexBufferManager.Dispose();
			_renderTarget.Dispose();
			_graphics.Dispose();
			_app.Dispose();
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	struct Vertex
	{
		public Vector4 Position;
		public Color4 Color;

		public Vertex(Vector4 position, Color4 color)
		{
			Position = position;
			Color = color;
		}

		/// <summary>
		/// Defines values that we'll use to tell the vertex shader how
		/// to map our vertex structure to the shader's input structure
		/// </summary>
		public static readonly ShaderInputElementSpec[] ShaderInputLayout =
		{
			ShaderInputElementPurpose.Position.CreateSpec(),
			ShaderInputElementPurpose.Color.CreateSpec()
		};
	}
}

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
using SimpleInstancing.Properties;

namespace SimpleInstancing
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
		private readonly IVertexBufferManager<Matrix4x4> _instanceBufferManager;

		public Program()
		{
			_app = new GrasshopperApp().UseSharpDX();
			_graphics = _app.Graphics.CreateContext(enableDebugMode: true);
			_renderTarget = _graphics.RenderTargetFactory.CreateWindow();
			_vertexBufferManager = _graphics.VertexBufferManagerFactory.Create<Vertex>();
			_instanceBufferManager = _graphics.VertexBufferManagerFactory.Create<Matrix4x4>();

			_renderTarget.Window.Title = "Instanced Quads";
			_renderTarget.Window.Visible = true;
		}

		private void Run()
		{
			CreateAndActivateDefaultMaterial();

			var vertexCount = CreateAndActivateVertexBuffer();
			var instanceCount = CreateAndActivateInstanceBuffer();

			// Now that our material and vertex buffers are activated
			// and ready to go, let's initiate our main loop and draw
			// our quad!
			_app.Run(_renderTarget, context =>
			{
				context.Clear(Color.CornflowerBlue);
				context.SetDrawType(DrawType.Triangles);
				context.DrawInstanced(vertexCount, instanceCount, 0, 0);
				context.Present();
			});
		}

		private void CreateAndActivateDefaultMaterial()
		{
			// Prepare our default material which will simply render
			// out using the vertex colour. We then set the material
			// active, which sets the active shaders in GPU memory,
			// ready for drawing with.
			var material = _graphics.MaterialManager.Create("simple");
			material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, Vertex.ShaderInputLayout, new[]
			{
				// Specify the shader input layout for each instance
				// of the quad that we want to draw many instances of
				new ShaderInputElementSpec(ShaderInputElementFormat.Matrix4x4),
			});
			material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
			material.Activate();
		}

		private int CreateAndActivateInstanceBuffer()
		{
			// We want to draw the same mesh multiple times, with
			// slightly different data for each instance, so we'll
			// define an array of four transformation matrices that
			// will be provide the data for each of four instances
			// we'll draw.
			var scaled = (Matrix4x4.Identity * 0.5f);
			scaled.M44 = 1.0f;
			const float dist = .4f;
			var instances = new[]
			{
				scaled * Matrix4x4.CreateTranslation(-dist, dist, 0),
				scaled * Matrix4x4.CreateTranslation(dist, dist, 0),
				scaled * Matrix4x4.CreateTranslation(-dist, -dist, 0),
				scaled * Matrix4x4.CreateTranslation(dist, -dist, 0)
			};

			// We also need a buffer to store the transformation
			// matrices for each quad instance that we want to render.
			var instanceBuffer = _instanceBufferManager.Create("quad");

			// Now we'll write our instance data to the instance
			// buffer that we created earlier
			using(var writer = instanceBuffer.BeginWrite(instances.Length))
				writer.Write(instances);

			// Instance buffers must be activated starting at slot #1
			instanceBuffer.Activate(1);

			return instances.Length;
		}

		private int CreateAndActivateVertexBuffer()
		{
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

			// Base vertex buffers must always be activated in slot #0
			vertexBuffer.Activate(0);

			return vertices.Length;
		}

		public void Dispose()
		{
			_vertexBufferManager.Dispose();
			_instanceBufferManager.Dispose();
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

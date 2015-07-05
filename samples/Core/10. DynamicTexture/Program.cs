using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using DynamicTexture.Properties;
using Grasshopper;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Primitives;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Procedural.Graphics.Primitives;
using Grasshopper.SharpDX;

namespace DynamicTexture
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
        private IDynamicTexture2DResource _texture;

        public Program()
        {
            _app = new GrasshopperApp().UseSharpDX();
            _graphics = _app.Graphics.CreateContext(enableDebugMode: true);
            _renderTarget = _graphics.RenderTargetFactory.CreateWindow();
            _vertexBufferManager = _graphics.VertexBufferManagerFactory.Create<Vertex>();

            _renderTarget.Window.Title = "Dynamic Texture";
            _renderTarget.Window.Visible = true;
        }

        private void Run()
        {
            // Choose sampler settings that will prevent our texture from blurring when it is scaled up.
            _graphics.TextureSamplerManager
                .Create("crisp", new TextureSamplerSettings(TextureWrapping.Clamp, TextureWrapping.Clamp, TextureWrapping.Clamp, TextureFiltering.MinMagMipPoint))
                .Activate(0);

            // Create a texture that we can write to, making sure the dimensions are a power of 2
            _texture = _graphics.TextureResourceManager.Create2DDynamic("foo", 8, 8, PixelFormat.R32G32B32A32_Float);
            _texture.Activate(0);

            // Prepare our default material with which we will render our dynamic texture. We then set the material
            // active, which sets the active shaders in GPU memory, ready for drawing with.
            var material = _graphics.MaterialManager.Create("simple");
            material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, Vertex.ShaderInputLayout);
            material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
            material.Textures.Add("foo");
            material.Activate();

            // We'll use Grasshopper's procedural tools to quickly build a set of vertices for our quad
            var vertices = QuadBuilder.New
                .XY()
                .Colors(Color.Red, Color.Green, Color.Blue, Color.Yellow)
                .Select(v => new Vertex(v.Position, v.Color, v.TextureCoordinate))
                .ToArray();

            // Create a vertex buffer. We don't need to dispose of it ourselves; it'll automatically get cleaned
            // up when the vertex manager that created it is disposed of.
            var vertexBuffer = _vertexBufferManager.Create("quad");

            // Six vertices will be written here; 3 per triangle
            using(var writer = vertexBuffer.BeginWrite(vertices.Length))
                writer.Write(vertices);

            // Normal vertex buffers should be activated in slot 0; we only use other slots when doing more
            // advanced things with shaders.
            vertexBuffer.Activate(0);

            // Now that our material and vertex buffers are activated and ready to go, let's initiate our main
            // loop and draw our quad!
            _app.Run(_renderTarget, (frame, context) =>
            {
                UpdateTexture(frame);

                context.Clear(Color.CornflowerBlue);
                context.SetDrawType(DrawType.Triangles);
                context.Draw(vertices.Length, 0);
                context.Present();
            });
        }

        private void UpdateTexture(FrameContext frame)
        {
            var rand = new Random((int)(frame.ElapsedSeconds*10));
            using(var writer = _texture.BeginWrite())
            {
                for(var y = 0; y < _texture.Height; y++)
                {
                    for(var x = 0; x < _texture.Width; x++)
                    {
                        var color = new Color4
                        {
                            Red = (float)rand.NextDouble(),
                            Green = (float)rand.NextDouble(),
                            Blue = (float)rand.NextDouble(),
                            Alpha = 1.0f,
                        };
                        writer.Write(color);
                    }
                }
            }
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
        public Vector2 TextureCoordinate;

        public Vertex(Vector4 position, Color4 color, Vector2 textureCoordinate)
        {
            Position = position;
            Color = color;
            TextureCoordinate = textureCoordinate;
        }

        /// <summary>
        /// Defines values that we'll use to tell the vertex shader how to map our vertex structure to the
        /// shader's input structure
        /// </summary>
        public static readonly ShaderInputElementSpec[] ShaderInputLayout =
        {
            ShaderInputElementPurpose.Position.CreateSpec(),
            ShaderInputElementPurpose.Color.CreateSpec(),
            ShaderInputElementPurpose.TextureCoordinate.CreateSpec(),
        };
    }
}

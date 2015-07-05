using System;
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
using Grasshopper.WindowsFileSystem;
using SimpleTexturedCube.Properties;

namespace SimpleTexturedCube
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
        private readonly IConstantBufferManager<Matrix4x4> _constantBufferManager;
        private readonly MeshBufferManager<Vertex> _meshBufferManager;

        public Program()
        {
            _app = new GrasshopperApp()
                .UseSharpDX()
                .UseWindowsFileSystem();
            _graphics = _app.Graphics.CreateContext(enableDebugMode: true);
            _renderTarget = _graphics.RenderTargetFactory.CreateWindow();
            _constantBufferManager = _graphics.ConstantBufferManagerFactory.Create<Matrix4x4>();
            _meshBufferManager = new MeshBufferManager<Vertex>(_graphics);

            _renderTarget.Window.Title = "Simple Textured Cube";
            _renderTarget.Window.Visible = true;
        }

        public void Run()
        {
            CreateAndActivateDefaultMaterial();

            // Use Grasshopper's procedural tools to create a cube mesh
            var mesh = CubeBuilder.New.ToMesh("cube",
                v => new Vertex(v.Position, v.Color, v.TextureCoordinate));

            // A mesh buffer manager allows us to pack multiple meshes into a vertex buffer and accompanying index
            // buffer, and keep track of the offset locations of each mesh in each buffer. In our case we're only
            // storing one mesh.
            var buffer = _meshBufferManager.Create("default", mesh);
            buffer.Activate();
            var cubeData = buffer["cube"];

            // Create and activate our constant buffer which will hold the final world-view-projection matrix for
            // use by the vertex shader
            var constantBuffer = _constantBufferManager.Create("cube");
            constantBuffer.Activate(0);

            // Create our initial view and projection matrices that will represent the camera
            var view = Matrix4x4.CreateLookAt(new Vector3(0, 1.25f, 3f), new Vector3(0, 0, 0), Vector3.UnitY);
            var proj = CreateProjectionMatrix(_renderTarget.Window);
            var viewproj = view * proj;

            // The aspect ratio changes when the window is resized, so we need to reculate the projection matrix,
            // or our cube will look squashed
            _renderTarget.Window.SizeChanged += win => viewproj = view * CreateProjectionMatrix(win);

            // Let's define an arbitrary rotation speed for the cube
            const float rotationsPerSecond = .04f;
            const float twoPI = (float)Math.PI * 2;

            // Let the looping begin!
            _app.Run(_renderTarget, (frame, context) =>
            {
                // Each frame we need to update the cube's rotation, then push the changes to the constant buffer
                var world = Matrix4x4.CreateRotationY(_app.ElapsedSeconds * rotationsPerSecond * twoPI);
                var wvp = Matrix4x4.Transpose(world * viewproj);
                constantBuffer.Update(wvp);

                context.Clear(Color.CornflowerBlue);
                context.SetDrawType(cubeData.DrawType);
                context.DrawIndexed(cubeData.IndexCount, cubeData.IndexBufferOffset, cubeData.VertexBufferOffset);
                context.Present();
            });
        }

        private void CreateAndActivateDefaultMaterial()
        {
            // Prepare a texture for use by the material below
            _graphics.TextureResourceManager.Create2DFromFile("rabbit", "Textures/rabbit.jpg");

            // Prepare our default material which will simply render out using the vertex colour. We then set the
            // material active, which sets the active shaders in GPU memory, ready for drawing with.
            var material = _graphics.MaterialManager.Create("simple");
            material.VertexShaderSpec = new VertexShaderSpec(Resources.VertexShader, Vertex.ShaderInputLayout);
            material.PixelShaderSpec = new PixelShaderSpec(Resources.PixelShader);
            material.Textures.Add("rabbit");
            material.Activate();
        }

        static Matrix4x4 CreateProjectionMatrix(IAppWindow window)
        {
            const float fieldOfView = 0.65f;
            const float nearPlane = 0.1f;
            const float farPlane = 1000f;
            var aspectRatio = (float)window.ClientWidth / window.ClientHeight;
            return Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlane, farPlane);
        }

        public void Dispose()
        {
            _constantBufferManager.Dispose();
            _meshBufferManager.Dispose();
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

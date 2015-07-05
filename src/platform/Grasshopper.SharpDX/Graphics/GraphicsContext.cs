using System;
using Grasshopper.Graphics;
using Grasshopper.Graphics.Materials;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Graphics.Rendering.Blending;
using Grasshopper.Graphics.Rendering.Buffers;
using Grasshopper.Graphics.Rendering.Rasterization;
using Grasshopper.Input;
using Grasshopper.Platform;
using Grasshopper.SharpDX.Graphics.Materials;
using Grasshopper.SharpDX.Graphics.Rendering;
using Grasshopper.SharpDX.Graphics.Rendering.Buffers;

namespace Grasshopper.SharpDX.Graphics
{
    class GraphicsContext : IGraphicsContext
    {
        public GraphicsContext(Lazy<IFileStore> files, IInputContext input)
        {
            var deviceManager = new DeviceManager();
            DeviceManager = deviceManager;
            
            TextureResourceManager = new TextureResourceManager(deviceManager, files);
            TextureSamplerManager = new TextureSamplerManager(deviceManager);
            MaterialManager = new MaterialManager(this);
            BlendStateManager = new BlendStateManager(deviceManager);
            RasterizerStateManager = new RasterizerStateManager(deviceManager);

            RenderTargetFactory = new RenderTargetFactory(this, input);
            VertexBufferManagerFactory = new VertexBufferManagerFactory(deviceManager);
            IndexBufferManagerFactory = new IndexBufferManagerFactory(deviceManager);
            ConstantBufferManagerFactory = new ConstantBufferManagerFactory(deviceManager);
        }

        IDeviceManager IGraphicsContext.DeviceManager { get { return DeviceManager; } }
        public DeviceManager DeviceManager { get; private set; }

        public ITextureResourceManager TextureResourceManager { get; private set; }
        public ITextureSamplerManager TextureSamplerManager { get; private set; }
        public IMaterialManager MaterialManager { get; private set; }
        public IBlendStateManager BlendStateManager { get; private set; }
        public IRasterizerStateManager RasterizerStateManager { get; private set; }

        public IRenderTargetFactory RenderTargetFactory { get; private set; }
        public IVertexBufferManagerFactory VertexBufferManagerFactory { get; private set; }
        public IIndexBufferManagerFactory IndexBufferManagerFactory { get; private set; }
        public IConstantBufferManagerFactory ConstantBufferManagerFactory { get; private set; }

        public void Initialize()
        {
            DeviceManager.Initialize();
            
            if(!TextureSamplerManager.Exists("default"))
                TextureSamplerManager.Create("default", TextureSamplerSettings.Default()).Activate(0);
            if(!BlendStateManager.Exists("none"))
                BlendStateManager.Create("none", BlendSettings.None()).Activate();
            if(!BlendStateManager.Exists("default"))
                BlendStateManager.Create("default", BlendSettings.DefaultEnabled());
            if(!RasterizerStateManager.Exists("default"))
                RasterizerStateManager.Create("default", RasterizerSettings.Default()).Activate();
        }

        private bool _disposed;
        public void Dispose()
        {
            if(_disposed) return;
            DeviceManager.Dispose();
            MaterialManager.Dispose();
            TextureResourceManager.Dispose();
            TextureSamplerManager.Dispose();
            BlendStateManager.Dispose();
            RasterizerStateManager.Dispose();
            _disposed = true;
        }
    }
}

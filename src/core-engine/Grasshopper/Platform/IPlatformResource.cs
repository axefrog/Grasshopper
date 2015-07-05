using System;

namespace Grasshopper.Platform
{
    /// <summary>
    /// Represents a resource that is controlled by the current selected platform. Examples include
    /// buffers and shaders on the GPU, audio resources, and other resources that take significant
    /// hardware resources. These may need to be unloaded and reloaded in certain circumstances and
    /// always need to be disposed of correctly when no longer needed.
    /// </summary>
    public interface IPlatformResource : IDisposable
    {
        string Id { get; }
        bool IsInitialized { get; }
        bool IsDisposed { get; }
        void Initialize();
        void Uninitialize();
        event PlatformResourceEventHandler Initialized;
        event PlatformResourceEventHandler Uninitialized;
        event PlatformResourceEventHandler Disposed;
    }

    public delegate void PlatformResourceEventHandler(IPlatformResource resource);
}
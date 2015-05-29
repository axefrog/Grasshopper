namespace Grasshopper.Platform
{
	public abstract class PlatformResource : IPlatformResource
	{
		protected PlatformResource(string id)
		{
			Id = id;
		}

		public string Id { get; private set; }
		public bool IsInitialized { get; private set; }
		public bool IsDisposed { get; private set; }

		public void Initialize()
		{
			if(IsInitialized)
				Uninitialize();

			InitializeInternal();
			
			var handler = Initialized;
			if(handler != null)
				handler(this);
			
			IsInitialized = true;
		}

		public void Uninitialize()
		{
			UninitializeInternal();

			var handler = Uninitialized;
			if(handler != null)
				handler(this);

			IsInitialized = false;
		}

		protected abstract void InitializeInternal();
		protected abstract void UninitializeInternal();

		public event PlatformResourceEventHandler Initialized;
		public event PlatformResourceEventHandler Uninitialized;
		public event PlatformResourceEventHandler Disposed;

		public void Dispose()
		{
			if(IsDisposed)
				return;

			Uninitialize();
			
			var handler = Disposed;
			if(handler != null)
				handler(this);

			IsDisposed = true;
		}
	}
}
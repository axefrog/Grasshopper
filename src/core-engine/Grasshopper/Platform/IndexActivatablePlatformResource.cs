using System;

namespace Grasshopper.Platform
{
	public abstract class IndexActivatablePlatformResource : PlatformResource, IIndexActivatablePlatformResource
	{
		protected IndexActivatablePlatformResource(string id) : base(id)
		{
		}

		public event IndexActivatablePlatformResourceEventHandler Activated;

		public void Activate(int index)
		{
			if(!IsInitialized)
				Initialize();

			ActivateAtIndex(index);
			NotifyActivated(index);
		}

		protected void NotifyActivated(int index)
		{
			var handler = Activated;
			if(handler != null)
				handler(this, index);
		}

		protected abstract void ActivateAtIndex(int index);
		
		public void SetActivatedExternally(int index)
		{
			NotifyActivated(index);
		}
	}
}
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
				throw new InvalidOperationException("Cannot ");
			ActivateAtIndex(index);
			NotifyAssigned(index);
		}

		protected void NotifyAssigned(int index)
		{
			var handler = Activated;
			if(handler != null)
				handler(this, index);
		}

		protected abstract void ActivateAtIndex(int index);
		
		public void SetActivatedExternally(int index)
		{
			NotifyAssigned(index);
		}
	}
}
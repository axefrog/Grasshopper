using System;

namespace Grasshopper.Platform
{
    public abstract class ActivatablePlatformResource : PlatformResource, IActivatablePlatformResource
    {
        protected ActivatablePlatformResource(string id) : base(id)
        {
        }

        public event ActivatablePlatformResourceEventHandler Activated;
        
        public void Activate()
        {
            if(!IsInitialized)
                Initialize();

            SetActive();
            NotifyActivated();
        }

        protected void NotifyActivated()
        {
            var handler = Activated;
            if(handler != null)
                handler(this);
        }

        protected abstract void SetActive();
    }
}
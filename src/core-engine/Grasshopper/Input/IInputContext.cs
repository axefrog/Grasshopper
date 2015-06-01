using System;

namespace Grasshopper.Input
{
	public interface IInputContext : IDisposable
	{
		IObservable<MouseEvent> MouseEvents { get; }
		void PostMouseEvent(MouseEvent mouseEvent);
	}
}
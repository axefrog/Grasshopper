using System;

namespace Grasshopper.Input
{
	public interface IInputContext : IDisposable
	{
		IObservable<MouseEvent> MouseEvents { get; }
		IObservable<KeyboardEvent> KeyboardEvents { get; }
		void PostMouseEvent(MouseEvent mouseEvent);
		void PostKeyboardEvent(KeyboardEvent keyboardEvent);
	}
}
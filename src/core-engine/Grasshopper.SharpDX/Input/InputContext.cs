using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Grasshopper.Input;

namespace Grasshopper.SharpDX.Input
{
	class InputContext : IInputContext
	{
		private readonly Subject<MouseEvent> _subject = new Subject<MouseEvent>();

		public InputContext()
		{
			MouseEvents = _subject.AsObservable();
		}

		public IObservable<MouseEvent> MouseEvents { get; private set; }
		
		public void PostMouseEvent(MouseEvent mouseEvent)
		{
			_subject.OnNext(mouseEvent);
		}

		public void Dispose()
		{
			_subject.Dispose();
		}
	}
}

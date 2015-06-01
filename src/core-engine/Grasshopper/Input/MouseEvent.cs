using Grasshopper.Graphics.Rendering;

namespace Grasshopper.Input
{
	public class MouseEvent
	{
		public MouseEvent(IAppWindow window, MouseButton button, ButtonState buttonState, int x, int y)
		{
			Type = MouseEventType.Button;
			Window = window;
			Button = button;
			ButtonState = buttonState;
			X = x;
			Y = y;
		}

		public MouseEvent(IAppWindow window, int deltaX, int deltaY, int x, int y)
		{
			Type = MouseEventType.Move;
			Window = window;
			DeltaX = deltaX;
			DeltaY = deltaY;
			X = x;
			Y = y;
		}

		public MouseEvent(IAppWindow window, int wheelDelta, int x, int y)
		{
			Type = MouseEventType.Wheel;
			Window = window;
			WheelDelta = wheelDelta;
			X = x;
			Y = y;
		}

		public IAppWindow Window { get; private set; }
		public MouseEventType Type { get; private set; }
		public MouseButton Button { get; private set; }
		public ButtonState ButtonState { get; private set; }
		public int DeltaX { get; private set; }
		public int DeltaY { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }
		public int WheelDelta { get; private set; }
	}

	public enum MouseEventType
	{
		Button,
		Wheel,
		Move
	}

	public enum MouseButton
	{
		Left = 1,
		Right,
		Middle,
		Extra1,
		Extra2
	}

	public enum ButtonState
	{
		Up,
		Down
	}
}

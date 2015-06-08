using System;
using System.Numerics;
using System.Reactive.Linq;
using Grasshopper.Input;

namespace CompositingSample.App
{
	class CubesInput
	{
		private readonly CubesDemo _app;
		float _prevElapsedSeconds;
		float? _currentTravelSpeed = 0;
		float? _currentRotationSpeed = 0;
		Throttle _throttle = Throttle.None;
		Rotation _rotation = Rotation.None;
		bool _isFocusLocked = true;
		private bool _userRequestedExit;

		const float ForwardSpeed = 8;
		const float BackwardSpeed = 5;
		const float RotationSpeed = 2;
		
		public CubesInput(CubesDemo app)
		{
			_app = app;
			app.Window.SetCursorPositionToCenter();
			app.Window.LockCursor = true;
			app.Window.ShowCursor = false;

			// Grasshopper uses Reactive Extensions to expose input events as an
			// observable stream. In your own projects, you'll need to make sure
			// the Reactive Extensions NuGet packages are referenced in order to
			// access all the extension methods for observables.

			// Handle the two possible results of pressing the ESC key
			app.Input.KeyboardEvents
				.Where(ev => ev.Key == Key.Escape && ev.State == KeyState.Down)
				.Subscribe(ev =>
				{
					if(_isFocusLocked) // unlock the input controls from the view
					{
						app.Window.LockCursor = false;
						app.Window.ShowCursor = true;
						_isFocusLocked = false;
					}
					else
						_userRequestedExit = true;
				});

			// Create observables for keypresses that only trigger when the controls are locked to the view
			var whenKeyHeldDown = app.Input.KeyboardEvents.Where(ev => ev.State == KeyState.Down && _isFocusLocked);
			var whenKeyReleased = app.Input.KeyboardEvents.Where(ev => ev.State == KeyState.Up && _isFocusLocked);

			// Handle keyboard events for forward, backward, roll left and roll right
			Func<KeyboardEvent, bool> isForwardKey = ev => ev.Key == Key.Up || ev.Key == Key.W;
			Func<KeyboardEvent, bool> isBackwardKey = ev => ev.Key == Key.Down || ev.Key == Key.S;
			Func<KeyboardEvent, bool> isRollLeftKey = ev => ev.Key == Key.Left || ev.Key == Key.A;
			Func<KeyboardEvent, bool> isRollRightKey = ev => ev.Key == Key.Right || ev.Key == Key.D;

			whenKeyHeldDown.Where(isForwardKey).Subscribe(ev => _throttle = Throttle.Forward);
			whenKeyHeldDown.Where(isBackwardKey).Subscribe(ev => _throttle = Throttle.Backward);
			whenKeyHeldDown.Where(isRollLeftKey).Subscribe(ev => _rotation = Rotation.Left);
			whenKeyHeldDown.Where(isRollRightKey).Subscribe(ev => _rotation = Rotation.Right);

			whenKeyReleased.Where(isForwardKey).Subscribe(ev => _throttle = Throttle.None);
			whenKeyReleased.Where(isBackwardKey).Subscribe(ev => _throttle = Throttle.None);
			whenKeyReleased.Where(isRollLeftKey).Subscribe(ev => _rotation = Rotation.None);
			whenKeyReleased.Where(isRollRightKey).Subscribe(ev => _rotation = Rotation.None);

			// Handle mouse events for looking around ("free look")
			var whenMouseMoved = app.Input.MouseEvents
				.Where(ev => ev.Type == MouseEventType.Move && ev.Window == null && _isFocusLocked);

			whenMouseMoved.Where(ev => ev.DeltaX != 0)
				.Subscribe(ev => app.Camera.Yaw(-(float)(ev.DeltaX * Math.PI / 180)/7.5f));

			whenMouseMoved.Where(ev => ev.DeltaY != 0)
				.Subscribe(ev => app.Camera.Pitch((float)(ev.DeltaY * Math.PI / 180)/7.5f));

			// Handle the two possible results of clicking the mouse button
			app.Input.MouseEvents
				.Where(ev => ev.Type == MouseEventType.Button && ev.ButtonState == ButtonState.Down)
				.Subscribe(ev =>
				{
					if(_isFocusLocked) // reset the camera and position
					{
						app.Camera.Position = Vector3.Zero;
						app.Camera.Direction = -Vector3.UnitZ;
						app.Camera.Up = Vector3.UnitY;
					}
					else // lock the input controls back to the view
					{
						_isFocusLocked = true;
						app.Window.LockCursor = true;
						app.Window.ShowCursor = false;
						app.Window.SetCursorPositionToCenter();
					}
				});
		}

		public bool UserRequestedExit
		{
			get { return _userRequestedExit; }
		}

		public void Apply()
		{
			var deltaSec = _app.ElapsedSeconds - _prevElapsedSeconds;

			switch(_throttle)
			{
				case Throttle.Forward: _currentTravelSpeed = ForwardSpeed; break;
				case Throttle.Backward: _currentTravelSpeed = -BackwardSpeed; break;
				case Throttle.None: _currentTravelSpeed = null; break;
			}

			switch(_rotation)
			{
				case Rotation.Left: _currentRotationSpeed = -RotationSpeed; break;
				case Rotation.Right: _currentRotationSpeed = RotationSpeed; break;
				case Rotation.None: _currentRotationSpeed = null; break;
			}

			_prevElapsedSeconds = _app.ElapsedSeconds;
			if(_currentTravelSpeed.HasValue)
				_app.Camera.Move(deltaSec * _currentTravelSpeed.Value);
			if(_currentRotationSpeed.HasValue)
				_app.Camera.Roll(deltaSec * _currentRotationSpeed.Value);
		}
	}

	enum Throttle
	{
		None,
		Forward,
		Backward,
	}

	enum Rotation
	{
		None,
		Left,
		Right
	}

	enum MovementState
	{
		None,
		Accelerating,
		Decelerating,
		Constant
	}
}

using System;
using System.Numerics;
using System.Reactive.Linq;
using FreeLookCamera.App;
using Grasshopper.Input;
using Grasshopper.SharpDX;
using Grasshopper.WindowsFileSystem;

namespace FreeLookCamera
{
	class Program
	{
		static void Main(string[] args)
		{
			// This sample builds on the previous sample and implements simple controls
			// for navigating amongst the cubes. The mouse is used to look around and
			// the arrow keys, or AWSD keys, can be used to move forward and backward
			// and to roll left or right. Press ESC to unlock the mouse and keyboard
			// from the viewport, and ESC again to exit. Click in the viewport to lock
			// the mouse and keyboard back to the viewport. Click again to reset the
			// view and orientation back to the centre of the cubes.

			// Apart from some small tweaks, almost all of the code from the previous
			// sample has been refactored into a "CubesDemo" class to make it easy to
			// use that functionality without having it obscure new code for handling
			// camera manipulation and movement. If the code in the CubesDemo class is
			// not clear, check out Sample 07 for extensive code comments.

			using(var app = new CubesDemo(totalCubes: 100000)
				.UseSharpDX()
				.UseWindowsFileSystem())
			{
				app.InitializeResources();

				var isFocusLocked = true;
				var userRequestedExit = false;
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
						if(isFocusLocked) // unlock the input controls from the view
						{
							app.Window.LockCursor = false;
							app.Window.ShowCursor = true;
							isFocusLocked = false;
						}
						else
							userRequestedExit = true;
					});

				// Create observables for keypresses that only trigger when the controls are locked to the view
				var whenKeyHeldDown = app.Input.KeyboardEvents.Where(ev => ev.State == KeyState.Down && isFocusLocked);
				var whenKeyReleased = app.Input.KeyboardEvents.Where(ev => ev.State == KeyState.Up && isFocusLocked);

				// Handle keyboard events for forward, backward, roll left and roll right
				var throttle = Throttle.None;
				var rotation = Rotation.None;

				Func<KeyboardEvent, bool> isForwardKey = ev => ev.Key == Key.Up || ev.Key == Key.W;
				Func<KeyboardEvent, bool> isBackwardKey = ev => ev.Key == Key.Down || ev.Key == Key.S;
				Func<KeyboardEvent, bool> isRollLeftKey = ev => ev.Key == Key.Left || ev.Key == Key.A;
				Func<KeyboardEvent, bool> isRollRightKey = ev => ev.Key == Key.Right || ev.Key == Key.D;

				whenKeyHeldDown.Where(isForwardKey).Subscribe(ev => throttle = Throttle.Forward);
				whenKeyHeldDown.Where(isBackwardKey).Subscribe(ev => throttle = Throttle.Backward);
				whenKeyHeldDown.Where(isRollLeftKey).Subscribe(ev => rotation = Rotation.Left);
				whenKeyHeldDown.Where(isRollRightKey).Subscribe(ev => rotation = Rotation.Right);

				whenKeyReleased.Where(isForwardKey).Subscribe(ev => throttle = Throttle.None);
				whenKeyReleased.Where(isBackwardKey).Subscribe(ev => throttle = Throttle.None);
				whenKeyReleased.Where(isRollLeftKey).Subscribe(ev => rotation = Rotation.None);
				whenKeyReleased.Where(isRollRightKey).Subscribe(ev => rotation = Rotation.None);

				// Handle mouse events for looking around ("free look")
				var whenMouseMoved = app.Input.MouseEvents
					.Where(ev => ev.Type == MouseEventType.Move && ev.Window == null && isFocusLocked);

				whenMouseMoved.Where(ev => ev.DeltaX != 0)
					.Subscribe(ev => app.Camera.Yaw(-(float)(ev.DeltaX * Math.PI / 180)/7.5f));

				whenMouseMoved.Where(ev => ev.DeltaY != 0)
					.Subscribe(ev => app.Camera.Pitch((float)(ev.DeltaY * Math.PI / 180)/7.5f));

				// Handle the two possible results of clicking the mouse button
				app.Input.MouseEvents
					.Where(ev => ev.Type == MouseEventType.Button && ev.ButtonState == ButtonState.Down)
					.Subscribe(ev =>
					{
						if(isFocusLocked) // reset the camera and position
						{
							app.Camera.Position = Vector3.Zero;
							app.Camera.Direction = -Vector3.UnitZ;
							app.Camera.Up = Vector3.UnitY;
						}
						else // lock the input controls back to the view
						{
							isFocusLocked = true;
							app.Window.LockCursor = true;
							app.Window.ShowCursor = false;
							app.Window.SetCursorPositionToCenter();
						}
					});

				var prevElapsedSeconds = 0f;
				float? currentTravelSpeed = 0;
				float? currentRotationSpeed = 0;
				
				const float forwardSpeed = 8;
				const float backwardSpeed = 5;
				const float rotationSpeed = 2;
				
				app.Run(() =>
				{
					var deltaSec = app.ElapsedSeconds - prevElapsedSeconds;

					switch(throttle)
					{
						case Throttle.Forward: currentTravelSpeed = forwardSpeed; break;
						case Throttle.Backward: currentTravelSpeed = -backwardSpeed; break;
						case Throttle.None: currentTravelSpeed = null; break;
					}

					switch(rotation)
					{
						case Rotation.Left: currentRotationSpeed = -rotationSpeed; break;
						case Rotation.Right: currentRotationSpeed = rotationSpeed; break;
						case Rotation.None: currentRotationSpeed = null; break;
					}

					prevElapsedSeconds = app.ElapsedSeconds;
					if(currentTravelSpeed.HasValue)
						app.Camera.Move(deltaSec * currentTravelSpeed.Value);
					if(currentRotationSpeed.HasValue)
						app.Camera.Roll(deltaSec * currentRotationSpeed.Value);

					return !userRequestedExit;
				});
			}
		}

		private enum Throttle
		{
			None,
			Forward,
			Backward,
		}

		private enum Rotation
		{
			None,
			Left,
			Right
		}

		private enum MovementState
		{
			None,
			Accelerating,
			Decelerating,
			Constant
		}
	}
}

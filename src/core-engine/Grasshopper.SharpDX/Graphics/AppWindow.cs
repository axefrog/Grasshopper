using System;
using System.Diagnostics;
using System.Windows.Forms;
using Grasshopper.Graphics.Rendering;
using Grasshopper.Input;
using SharpDX.Windows;
using ButtonState = Grasshopper.Input.ButtonState;

namespace Grasshopper.SharpDX.Graphics
{
	class AppWindow : IAppWindow
	{
		private readonly IInputContext _input;
		private readonly RenderForm _form;
		private bool _showBordersAndTitle;
		private readonly RenderLoop _renderLoop;
		private bool _resizable;
		private int _lastX, _lastY;
		private bool _lostFocus = true;

		public AppWindow(IInputContext input)
		{
			_input = input;
			_form = new RenderForm();
			_renderLoop = new RenderLoop(Form);

			_form.Resize += OnFormResize;
			_form.Closed += OnFormClosed;
			_form.LostFocus += OnLostFocus;
			_form.MouseMove += OnMouseMove;
			_form.MouseDown += OnMouseDown;
			_form.MouseUp += OnMouseUp;
			_form.MouseWheel += OnMouseWheel;
		}

		private void OnFormResize(object sender, EventArgs eventArgs)
		{
			var handler = SizeChanged;
			if(handler != null)
				handler(this);
		}

		private void OnFormClosed(object sender, EventArgs eventArgs)
		{
			var handler = Closed;
			if(handler != null)
				handler(this);
		}

		private void OnLostFocus(object sender, EventArgs eventArgs)
		{
			_lostFocus = true;
		}

		private void OnMouseMove(object sender, MouseEventArgs args)
		{
			int deltaX, deltaY;
			if(_lostFocus)
			{
				deltaX = 0;
				deltaY = 0;
				_lostFocus = false;
			}
			else
			{
				deltaX = args.X - _lastX;
				deltaY = args.Y - _lastY;
			}
			_lastX = args.X;
			_lastY = args.Y;
			_input.PostMouseEvent(new MouseEvent(this, deltaX, deltaY, args.X, args.Y));
		}

		private bool TryGetMouseButton(MouseButtons button, out MouseButton result)
		{
			switch(button)
			{
				case MouseButtons.Left: result = MouseButton.Left; return true;
				case MouseButtons.Middle: result = MouseButton.Middle; return true;
				case MouseButtons.Right: result = MouseButton.Right; return true;
				case MouseButtons.XButton1: result = MouseButton.Extra1; return true;
				case MouseButtons.XButton2: result = MouseButton.Extra2; return true;
			}
			result = 0;
			return false;
		}

		private void OnMouseDown(object sender, MouseEventArgs args)
		{
			MouseButton button;
			if(!TryGetMouseButton(args.Button, out button))
				return;
			_lastX = args.X;
			_lastY = args.Y;
			_input.PostMouseEvent(new MouseEvent(this, button, ButtonState.Down, _lastX, _lastY));
		}

		private void OnMouseUp(object sender, MouseEventArgs args)
		{
			MouseButton button;
			if(!TryGetMouseButton(args.Button, out button))
				return;
			_lastX = args.X;
			_lastY = args.Y;
			_input.PostMouseEvent(new MouseEvent(this, button, ButtonState.Up, _lastX, _lastY));
		}

		private void OnMouseWheel(object sender, MouseEventArgs args)
		{
			_lastX = args.X;
			_lastY = args.Y;
			_input.PostMouseEvent(new MouseEvent(this, args.Delta, _lastX, _lastY));
		}

		public event AppWindowSimpleEventHandler SizeChanged;
		public event AppWindowSimpleEventHandler Closed;

		public int Left
		{
			get { return Form.Left; }
			set { Form.Left = value; }
		}

		public int Top
		{
			get { return Form.Top; }
			set { Form.Top = value; }
		}

		public int Width
		{
			get { return Form.Width; }
			set { Form.Width = value; }
		}

		public int Height
		{
			get { return Form.Height; }
			set { Form.Height = value; }
		}

		public int ClientWidth { get { return Form.ClientSize.Width; } }
		public int ClientHeight { get { return Form.ClientSize.Height; } }

		public string Title
		{
			get { return Form.Text; }
			set { Form.Text = value; }
		}

		private void UpdateFormBorderStyle()
		{
			Form.FormBorderStyle = ShowBordersAndTitle
				? Resizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog
				: FormBorderStyle.None;
		}

		public bool Resizable
		{
			get { return _resizable; }
			set
			{
				_resizable = value;
				UpdateFormBorderStyle();
			}
		}

		public bool ShowBordersAndTitle
		{
			get { return _showBordersAndTitle; }
			set
			{
				_showBordersAndTitle = value;
				UpdateFormBorderStyle();
			}
		}

		// todo: implement fullscreen rendering (will require alt-enter handling)
		public bool PreferWindowedFullScreen { get; set; }

		public bool Visible
		{
			get { return Form.Visible; }
			set { Form.Visible = value; }
		}

		public RenderForm Form
		{
			get { return _form; }
		}

		public void SetSize(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public void SetFullScreen(bool enabled = true, bool windowed = false)
		{

		}

		public bool NextFrame()
		{
			return _renderLoop.NextFrame();
		}

		public bool NextFrame(AppWindowFrameExecutionHandler run)
		{
			return NextFrame() && run(this);
		}

		public void Dispose()
		{
			_renderLoop.Dispose();
			Form.Dispose();
		}
	}
}

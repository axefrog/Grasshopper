using System.Windows.Forms;
using Grasshopper.Graphics;
using SharpDX.Windows;

namespace Grasshopper.SharpDX.Graphics
{
	public class AppWindow : IAppWindow
	{
		private readonly RenderForm _form;
		private bool _showBordersAndTitle;
		private readonly RenderLoop _renderLoop;
		private bool _resizable;

		public AppWindow()
		{
			_form = new RenderForm();
			_renderLoop = new RenderLoop(Form);

			_form.SizeChanged += (sender, args) =>
			{
				var handler = SizeChanged;
				if(handler != null)
					handler(this);
			};
		}

		public event AppWindowSimpleEventHandler SizeChanged;

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

		public bool NextFrame(AppWindowFrameExecutionHandler run)
		{
			return _renderLoop.NextFrame() && run(this);
		}

		public void Dispose()
		{
			_renderLoop.Dispose();
			Form.Dispose();
		}
	}
}

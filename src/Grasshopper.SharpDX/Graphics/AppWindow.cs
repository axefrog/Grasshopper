using System;
using System.Windows.Forms;
using Grasshopper.Graphics;
using SharpDX.Windows;

namespace Grasshopper.SharpDX.Graphics
{
	public class AppWindow : IAppWindow
	{
		private readonly RenderForm _form;
		private bool _showBordersAndTitle;
		private bool _resizable;
		private RenderLoop _renderLoop;

		public AppWindow()
		{
			_form = new RenderForm();
			_renderLoop = new RenderLoop(_form);
		}

		public int Left
		{
			get { return _form.Left; }
			set { _form.Left = value; }
		}

		public int Top
		{
			get { return _form.Top; }
			set { _form.Top = value; }
		}

		public int Width
		{
			get { return _form.Width; }
			set { _form.Width = value; }
		}

		public int Height
		{
			get { return _form.Height; }
			set { _form.Height = value; }
		}

		public int ClientWidth { get { return _form.ClientSize.Width; } }
		public int ClientHeight { get { return _form.ClientSize.Height; } }

		public string Title
		{
			get { return _form.Text; }
			set { _form.Text = value; }
		}

		private void UpdateFormBorderStyle()
		{
			_form.FormBorderStyle = ShowBordersAndTitle
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
			get { return _form.Visible; }
			set { _form.Visible = value; }
		}

		public void SetSize(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public void SetFullScreen(bool enabled = true, bool windowed = false)
		{
			
		}

		public bool NextFrame(NextFrameHandler run)
		{
			return _renderLoop.NextFrame() && run(this);
		}

		public void Dispose()
		{
			_form.Dispose();
		}
	}
}

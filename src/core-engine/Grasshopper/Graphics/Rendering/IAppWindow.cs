using System;

namespace Grasshopper.Graphics.Rendering
{
	public interface IAppWindow : IDisposable
	{
		int Left { get; set; }
		int Top { get; set; }
		int Width { get; set; }
		int Height { get; set; }
		int ClientWidth { get; }
		int ClientHeight { get; }
		string Title { get; set; }
		bool Resizable { get; set; }
		bool ShowBordersAndTitle { get; set; }
		bool PreferWindowedFullScreen { get; set; }
		bool Visible { get; set; }
		void SetSize(int width, int height);
		void SetFullScreen(bool enabled = true, bool windowed = false);
		bool ShowCursor { get; set; }
		bool LockCursor { get; set; }
		void SetCursorPosition(int x, int y);
		void SetCursorPositionToCenter();

		event AppWindowSimpleEventHandler SizeChanged;
		bool NextFrame(AppWindowFrameExecutionHandler run);
		bool NextFrame();
	}

	public delegate void AppWindowSimpleEventHandler(IAppWindow win);
	public delegate bool AppWindowFrameExecutionHandler(IAppWindow win);
}
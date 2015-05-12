using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Grasshopper.SharpDX;

namespace HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var app = SharpDXBootstrapper.CreateGrasshopperApp())
			using(var mainWindow = app.Services.Windows.Create())
			using(var otherWindow = app.Services.Windows.Create())
			{
				app.Run(() =>
				{
					return mainWindow.NextFrame(win =>
					{
						win.Title = "Hello, window #1! It's currently " + DateTime.UtcNow.ToString("F");
						win.Visible = true;
						return true;
					}) && otherWindow.NextFrame(win =>
					{
						win.Title = "Hello, window #2! It's currently " + DateTime.UtcNow.ToString("F");
						win.Visible = true;
						return true;
					});
				});
			}
		}
	}
}

using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Maui_Dig
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
	}
}
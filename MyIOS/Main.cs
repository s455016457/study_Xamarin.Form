using UIKit;

namespace MyIOS
{
	public class Application
	{
		// 这是应用程序的主要入口
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
            //如果你想要使用不懂与“APPDelegate”的应用程序委托类，你可以在这里指定
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
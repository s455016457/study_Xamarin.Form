using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace MyIOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    // 应用程序的UIApplicationDelegate。
    // 这个类不但负责开启应用程序的用户界面，而且还在IOS中监听应用程序的事件
    [Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
        // 声明类的级别

		public override UIWindow Window {
			get;
			set;
		}

        /// <summary>
        /// 完成开启
        /// </summary>
        /// <param name="application">应用程序</param>
        /// <param name="launchOptions">开启选项</param>
        /// <returns></returns>
		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            // 在应用程序启动后自定义的重写入口。
            // 如果你的应用程序中不需要，你可以放心的删除这个方法

            //请求来自用户的通知权限。
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, eer) =>
            {
                //审批语柄
            });
            //请求来自用户的通知权限。
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Badge, (approved, eer) =>
            {
                //审批语柄
            });
            //请求来自用户的通知权限。
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Sound, (approved, eer) =>
            {
                //审批语柄
            });

            // Watch for notifications while the app is active
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

            // 设置数据获取服务后台执行评率
            //UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(10);

            


            return true;
		}


        DateTime PerformFetch_oldDate = DateTime.Now;
        int count = 1;
        public override void PerformFetch(UIApplication application,  Action<UIBackgroundFetchResult> completionHandler)
        {
            // 获取数据，并显示他们
            if (DateTime.Now - PerformFetch_oldDate >= TimeSpan.FromSeconds(10))
            {
                var content = new UNMutableNotificationContent();
                content.Title = "PerformFetch_服务通知标题" + count;
                content.Subtitle = "PerformFetch_服务通知副标题" + count;
                content.Body = "PerformFetch_服务通知类容,这里可以有好多的内容" + count;
                content.Badge = count;

                //5秒后发送通知，不重复
                var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

                // 创建通知发送请求
                var requestID = "sampleRequest2";
                var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                // 添加通知发送请求
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
                {
                    if (err != null)
                    {
                        // 处理异常
                    }
                });
                count++;

                //通知系统获取结果。
                completionHandler(UIBackgroundFetchResult.NewData);
            }

            base.PerformFetch(application, completionHandler);
        }

        public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
            // 当应用程序刚从活跃状态进入不活跃状态时调用
            // 这会发生在某些类型的暂时中断（比如收到电话呼叫或SMS信息）或当用户退出应用程序并且应用程序开始进入到后台状态。
            // 游戏应该使用这个方法去暂停游戏
            
		}

		public override void DidEnterBackground (UIApplication application)
		{
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
            // 使用这个方法去释放公共资源，保存用户数据，使计时器无效和保存应用状态。
            // 如果应用程序支持后台执行，当用户退出是会调用这个方法而不是 WillTerminate 方法
        }

        public override void WillEnterForeground (UIApplication application)
		{
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
            // 从后台转入活动状态调用的部分
            // 在这里，你可以撤销在进入后台时所做的许多更改。
        }

        public override void OnActivated (UIApplication application)
		{
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
            // 重启任何暂停(或未启动)的任务，虽然应用程序是不活跃的
            // 如果应用程序之前在后台中，则可以随意刷新用户界面
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;//将徽章数量设置为0

        }

		public override void WillTerminate (UIApplication application)
		{
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
            // 当应用撤销即将终止时调用。可以保存数据。请参考 DidEnterBackground
        }
    }
}
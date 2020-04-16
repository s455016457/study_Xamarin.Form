using System;
using System.Threading;
using UIKit;
using UserNotifications;
using System.Text;
using MyIOS.Services;

namespace MyIOS
{
    public partial class FirstViewController : UIViewController
    {
        private static int count = 0;
        iOSLongRunningTaskExample service;

        public FirstViewController(IntPtr handle) : base(handle)
        {
            service = new iOSLongRunningTaskExample();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void UIButton487_TouchUpInside(UIButton sender)
        {
            count++;
            // 创建本地通知
            var content = new UNMutableNotificationContent();
            content.Title = "通知标题" + count;
            content.Subtitle = "通知副标题" + count;
            content.Body = "通知类容,这里可以有好多的内容" + count;
            content.Badge = count;

            //1秒后发送通知，不重复
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

            // 创建通知发送请求
            var requestID = "sampleRequest";
            var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

            // 添加通知发送请求
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
            {
                if (err != null)
                {
                    // 处理异常
                }
            });
        }

        partial void UIButton737_TouchUpInside(UIButton sender)
        {
            InvokeOnMainThread(() =>
            {
                MessageLabel.Text = "请稍后。。。";
            });
            // 检查应用程序是否有权限
            Boolean alertsAllowed = false, soundSetting = false, badgeSetting = false;
            UNUserNotificationCenter.Current.GetNotificationSettings((settings) =>
            {
                alertsAllowed = settings.AlertSetting == UNNotificationSetting.Enabled;
                soundSetting = settings.SoundSetting == UNNotificationSetting.Enabled;
                badgeSetting = settings.BadgeSetting == UNNotificationSetting.Enabled;
                InvokeOnMainThread(() =>
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    stringBuilder.AppendFormat("拥有弹出框的权限【{0}】", alertsAllowed);
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("拥有提示铃声的权限【{0}】", soundSetting);
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("拥有徽章的权限【{0}】", badgeSetting);

                    MessageLabel.Text = stringBuilder.ToString();
                });
            });
        }

        partial void StartService_TouchUpInside(UIButton sender)
        {
            service.Start();
        }

        partial void StopService_TouchUpInside(UIButton sender)
        {
            service.Stop();
        }
    }
}
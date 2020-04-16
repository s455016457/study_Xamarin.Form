using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyAndroid
{
    [Activity(Label = "TestNotificationActivity")]
    public class TestNotificationActivity : Activity
    {
        Button sendButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TestNotification);

            sendButton = FindViewById<Button>(Resource.Id.sendButton);
            sendButton.Click += SendButton_Click;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            int count = 1;

            Notification notification = new Notification()
            {
                Icon=Resource.Drawable.icon,
                Number= count++,
                Flags=NotificationFlags.AutoCancel,
                Vibrate=new long[] { 500, 500 },
            };
            Notification.Builder builder = Notification.Builder.RecoverBuilder(Application.Context, notification)
            .SetContentTitle("这里是标题")
            .SetContentText("这里是通知内容。。。。")
            .SetSmallIcon(Resource.Drawable.icon);

            // 获取通知管理
            NotificationManager notificationManager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            // 发送通知:
            notificationManager.Notify(200, notification);

        }
    }
}
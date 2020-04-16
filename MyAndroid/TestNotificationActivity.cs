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

            SetContentView(Resource.Layout.TestNotification);
            // Create your application here
            sendButton = FindViewById<Button>(Resource.Id.sendButton);
            sendButton.Click += SendButton_Click;
        }

        int count = 1;
        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Setup an intent for SecondActivity:
                Intent secondIntent = new Intent(this, typeof(MainActivity));

                // 传递些参数到 to TestNotificationActivity:
                secondIntent.PutExtra("message", "Greetings from TestNotificationActivity!");

                // 创建一个任务堆栈构建器来管理后退堆栈。
                TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);

                // 添加所有TestNotificationActivity的父Activity到堆栈中:
                stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
                stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(TestNotificationActivity)));

                // 将启动TestNotificationActivity的意图推到堆栈上。
                stackBuilder.AddNextIntent(secondIntent);

                //设置一个意图，以便利用通知返回该应用程序
                //Intent intent = new Intent(this, typeof(MainActivity));
                const int pendingIntentId = 0;
                //创建一个意图和目标操作的描述，PendingIntent只能被使用一次
                PendingIntent pendingIntent =
                    stackBuilder.GetPendingIntent( pendingIntentId, PendingIntentFlags.OneShot);

                Notification.Builder builder = new Notification.Builder(this)
                .SetContentIntent(pendingIntent)
                .SetContentTitle("这里是标题" + count)
                .SetContentText("这里是通知内容。。。。" + count)
                .SetVibrate(new long[] { 500, 500 })
                .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                .SetSmallIcon(Resource.Drawable.icon);

                Notification notification = builder.Build();
                notification.Number = count++;
                notification.Flags = NotificationFlags.AutoCancel;

                // 获取通知管理
                NotificationManager notificationManager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

                // 发送通知:
                notificationManager.Notify(1200, notification);
            }
            catch (Exception ex)
            {
                new AlertDialog.Builder(this)
                    .SetMessage(ex.Message);

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MyAndroid.MyServices
{
    /// <summary>
    /// 后台服务
    /// IsolatedProcess = true 设置服务与Android应用在程序相同的进程中启动
    /// </summary>
	[Service(Name = "com.xamarin.DemoService")]
    public class DemoService : Service, IGetTimestamp
    {
        IGetTimestamp timestamper;
        public ServiceBinder binder { get; private set; }
        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug("MyDebug", "服务创建");
            timestamper = new UtcTimestamper();
        }

#pragma warning disable CS0672 // 成员将重写过时的成员
        public override void OnStart(Intent intent, int startId)
#pragma warning restore CS0672 // 成员将重写过时的成员
        {
#pragma warning disable CS0618 // 类型或成员已过时
            base.OnStart(intent, startId);
#pragma warning restore CS0618 // 类型或成员已过时
            Log.Debug("MyDebug", "服务启动");
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Debug("MyDebug", "服务已启动");

            Timer timer = new Timer()
            {
                Interval=60*1000,
                Enabled=true,
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            return base.OnStartCommand(intent, flags, startId);
        }
        private int count = 1;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Setup an intent for SecondActivity:
            Intent secondIntent = new Intent(this, typeof(MainActivity));

            // 传递些参数到 to TestNotificationActivity:
            secondIntent.PutExtra("message", "Greetings from TestNotificationActivity!");

            // 创建一个任务堆栈构建器来管理后退堆栈。
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);

            // 添加所有TestNotificationActivity的父Activity到堆栈中:
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));

            // 将启动TestNotificationActivity的意图推到堆栈上。
            stackBuilder.AddNextIntent(secondIntent);

            //设置一个意图，以便利用通知返回该应用程序
            //Intent intent = new Intent(this, typeof(MainActivity));
            const int pendingIntentId = 0;
            //创建一个意图和目标操作的描述，PendingIntent只能被使用一次
            PendingIntent pendingIntent =
                stackBuilder.GetPendingIntent(pendingIntentId, PendingIntentFlags.OneShot);

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

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug("MyDebug", "服务绑定");
            binder = new ServiceBinder(this);
            return binder;
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            Log.Debug("MyDebug", "系统内存不足");
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            Log.Debug("MyDebug", "设备配置发生更改时由系统调用");
        }

        public override void OnRebind(Intent intent)
        {
            base.OnRebind(intent);
            Log.Debug("MyDebug", "当新客户端连接到服务时，在它之前被告知所有的客户都已断开其 Android.App.Service.OnUnbind(Android.Content.Intent)后，调用该服务。");
        }

        public override void OnTaskRemoved(Intent rootIntent)
        {
            base.OnTaskRemoved(rootIntent);
            Log.Debug("MyDebug", "如果服务当前正在运行，并且用户已经删除了来自服务应用程序的任务，则调用此操作。");
        }

        public override void OnTrimMemory([GeneratedEnum] TrimMemory level)
        {
            base.OnTrimMemory(level);
            Log.Debug("MyDebug", "当操作系统认为是处理进程不需要内存的时候调用");
        }

        public override bool OnUnbind(Intent intent)
        {
            Log.Debug("MyDebug", "当所有客户端与服务发布的特定接口断开连接时调用。");
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            binder = null;
            Log.Debug("MyDebug", "服务销毁");
        }

        public string GetFormattedTimestamp()
        {
            return timestamper?.GetFormattedTimestamp();
        }
    }
}
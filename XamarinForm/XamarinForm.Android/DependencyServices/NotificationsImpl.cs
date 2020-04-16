using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using XamarinForm.DependencyServices;
using XamarinForm.Droid.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationsImpl))]
namespace XamarinForm.Droid.DependencyServices
{
    public class NotificationsImpl : AbstractNotificationsImpl
    {
        private static readonly int ButtonClickNotificationId = 1000;
        readonly AlarmManager alarmManager;
        public static int AppIconResourceId { get; set; }


        static NotificationsImpl()
        {
            AppIconResourceId = Application.Context.Resources.GetIdentifier("icon", "drawable", Application.Context.PackageName);
        }


        public NotificationsImpl()
        {
            this.alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
        }

        public override Task<bool> RequestPermission() => Task.FromResult(true);

        public override Task<int> Send(XamarinForm.DependencyServices.Notification notification_Obj)
        {
            if (notification_Obj == null)
            {
                throw new ArgumentNullException(nameof(notification_Obj));
            }

            if (!notification_Obj.Id.HasValue) notification_Obj.Id = ButtonClickNotificationId;

            var launchIntent = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            launchIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            foreach (var pair in notification_Obj.Metadata)
            {
                launchIntent.PutExtra(pair.Key, pair.Value);
            }

            // 创建构造器
            Android.App.Notification.Builder builder = new Android.App.Notification.Builder(Application.Context)
                .SetContentTitle(notification_Obj.Title)
                .SetContentText(notification_Obj.Message)
                .SetSmallIcon(AppIconResourceId)
                .SetContentIntent(Android.Support.V4.App.TaskStackBuilder
                        .Create(Application.Context)
                        .AddNextIntent(launchIntent)
                        .GetPendingIntent(notification_Obj.Id.Value, (int)PendingIntentFlags.OneShot)
                    );

            if (notification_Obj.Vibrate)
            {
                builder.SetVibrate(new long[] { 500, 500 });
            }

            if (notification_Obj.When.HasValue)
            {
                builder.SetShowWhen(true);
                builder.SetWhen(notification_Obj.When.Value.Seconds);
            }

            if (notification_Obj.Sound != null)
            {
                if (!notification_Obj.Sound.Contains("://"))
                {
                    notification_Obj.Sound = $"{ContentResolver.SchemeAndroidResource}://{Application.Context.PackageName}/raw/{notification_Obj.Sound}";
                }
                var uri = Android.Net.Uri.Parse(notification_Obj.Sound);
                builder.SetSound(uri);
            }

            // 构造通知
            Android.App.Notification notification = builder.Build();

            // 获取通知管理
            NotificationManager notificationManager =  Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            // 发送通知:
            notificationManager.Notify(notification_Obj.Id.Value, notification);


            return Task.FromResult(notification_Obj.Id.Value);
        }
        
        public override void Vibrate(int ms = 300)
        {
            using (var vibrate = (Vibrator)Application.Context.GetSystemService(Context.VibratorService))
            {
                if (!vibrate.HasVibrator)
                    return;

                vibrate.Vibrate(ms);
            }
        }
    }
}
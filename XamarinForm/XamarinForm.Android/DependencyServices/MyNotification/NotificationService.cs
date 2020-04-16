using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinForm.DependencyServices.MyNotification;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinForm.Droid.DependencyServices.MyNotification.NotificationService))]
namespace XamarinForm.Droid.DependencyServices.MyNotification
{
    public class NotificationService : INotificationService
    {
        // 获取通知管理
        NotificationManager notificationManager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
        public void Clear(int Id)
        {
            notificationManager.Cancel(Id);
        }

        public void Clear()
        {
            notificationManager.CancelAll();
        }

        public Task<bool> RequestPermission() => Task.FromResult(true);

        public async Task Send(XamarinForm.DependencyServices.MyNotification.NotificationConfig notification)
        {
            var _context = Application.Context;

            Android.App.Notification _notification = await Task<Android.App.Notification>.Factory.StartNew(() =>
            {
                try
                {
                    return CreateAndroidNotification(notification);
                }
                catch (Exception ex)
                {
                    new AlertDialog.Builder(_context).SetMessage(ex.Message);
                };
                return null;
            });

            if (_notification == null) return;
            
            // 发送通知:
            notificationManager.Notify(notification.Id, _notification);
        }

        private Android.App.Notification CreateAndroidNotification(XamarinForm.DependencyServices.MyNotification.NotificationConfig notification)
        {
            var _context = Application.Context;
            // 设置一个跳转到Activity的意图
            Intent secondIntent = new Intent(_context, typeof(MainActivity));

            // 传递些参数到 to TestNotificationActivity:
            //secondIntent.PutExtra("message", "Greetings from TestNotificationActivity!");
            
            const int pendingIntentId = 0;
            //创建一个意图和目标操作的描述，PendingIntent只能被使用一次
            PendingIntent pendingIntent = PendingIntent.GetActivity(_context, pendingIntentId, secondIntent,PendingIntentFlags.OneShot);

            Android.App.Notification.Builder builder = new Android.App.Notification.Builder(_context)
            .SetContentIntent(pendingIntent)
            .SetContentTitle(notification.Title)
            .SetContentText(notification.Message)
            .SetSmallIcon(Resource.Drawable.icon);

            if (notification.IsVibrate && notification.IsSound)
            {
                builder.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate);
            }
            else if (notification.IsVibrate)
            {
                builder.SetDefaults(NotificationDefaults.Vibrate);
            }
            else if (notification.IsVibrate)
            {
                builder.SetDefaults(NotificationDefaults.Sound);
            }

            if (notification.VibratePattern != null && notification.VibratePattern.Count() == 2)
            {
                builder.SetVibrate(new long[] { 500, 500 });
            }

            if (!String.IsNullOrWhiteSpace(notification.SoundPath))
            {
                if (!notification.SoundPath.Contains("://"))
                {
                    notification.SoundPath = $"{ContentResolver.SchemeAndroidResource}://{Application.Context.PackageName}/raw/{notification.SoundPath}";
                }
                var uri = Android.Net.Uri.Parse(notification.SoundPath);
                builder.SetSound(uri);
            }


            Android.App.Notification _notification = builder.Build();
            _notification.Number = notification.Badge;
            _notification.Flags = NotificationFlags.AutoCancel;

            return _notification;
        }
    }
}
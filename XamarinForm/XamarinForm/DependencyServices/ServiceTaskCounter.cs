using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm;
using XamarinForm.DependencyServices.MyNotification;

namespace MyAndroid.MyServices
{
    public class ServiceTaskCounter : IDisposable
    {
        public static double DefaultInterval = 30 * 1000;

        public double Interval { get; private set; } = DefaultInterval;
        Task backgroundTask;
        System.Timers.Timer timer;

        public void Dispose()
        {
            timer.Dispose();
            backgroundTask.Dispose();
            GC.Collect();
        }

        public async Task RunCounter(CancellationToken token)
        {
            backgroundTask = Task.Run(() =>
            {
                timer = new System.Timers.Timer
                {
                    Enabled = true,
                    Interval = Interval,
                };

                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }, token);

            await backgroundTask;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var config = new NotificationConfig
            {
                Id = 1200,
                Title = "这里是通知标题",
                Message = "这里是通知正文",
                IsSound = true,
                IsVibrate = true,
                Badge = 1,
            };

            Device.BeginInvokeOnMainThread(() =>
            {
                MessagingCenter.Send<NotificationConfig>(config, "SendNotification");
            });
        }
    }
}
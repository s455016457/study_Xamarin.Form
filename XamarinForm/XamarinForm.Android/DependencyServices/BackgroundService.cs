using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinForm.DependencyServices;
using XamarinForm.Droid.DependencyServices;

[assembly: Dependency(typeof(BackgroundService))]
namespace XamarinForm.Droid.DependencyServices
{
    /// <summary>
    /// Android后台服务
    /// </summary>
    public class BackgroundService : IBackgroundService
    {
        /// <summary>
        /// 服务开启触发
        /// </summary>
        public event EventHandler OnDoStart;
        /// <summary>
        /// 服务停止触发
        /// </summary>
        public event EventHandler OnDoStop;

        public IBinder Binder { get; private set; }


        public bool IsStart { get; private set; } = false;

        private Context context = Android.App.Application.Context;
        Intent intent = new Intent(Android.App.Application.Context, typeof(MyService.BackgroundService));

        public void Start()
        {
            if (IsStart) return;

            try
            {
                //启动服务
                context.StartService(intent);

                IsStart = true;
                if (OnDoStart != null)
                    OnDoStart.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Log.Debug("", ex.Message);
                throw ex;
            }
        }

        public void Stop()
        {
            if (!IsStart) return;
            IsStart = false;
            context.StopService(intent);
            if (OnDoStop != null)
                OnDoStop.Invoke(this, new EventArgs());
        }

    }
}

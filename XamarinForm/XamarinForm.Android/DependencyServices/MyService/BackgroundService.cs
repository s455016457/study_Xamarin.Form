using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinForm.Droid.DependencyServices.MyService
{

    /// <summary>
    /// Android后台服务
    /// </summary>
    [Service]
    public class BackgroundService : Service, IGetTimestamp
    {
        public IBinder Binder { get; private set; }
        UtcTimestamper utcTimestamper;
        CancellationTokenSource _cts;
        Task backgroundTask;
        MyAndroid.MyServices.ServiceTaskCounter counter;

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            backgroundTask = Task.Run(() =>
            {
                //调用共享代码
                counter = new MyAndroid.MyServices.ServiceTaskCounter();
                counter.RunCounter(_cts.Token).Wait();
            }, _cts.Token);

            return base.OnStartCommand(intent, flags, startId);
        }

        public override IBinder OnBind(Intent intent)
        {
            Binder=new TimestampBinder(this);
            return Binder;
        }
       
        public override void OnDestroy()
        {
            utcTimestamper = null;
            backgroundTask.Dispose();
            counter.Dispose();
            base.OnDestroy();
        }

        /// <summary>
        /// 获取服务启动的时间
        /// </summary>
        /// <returns></returns>
        public string GetFormattedTimestamp()
        {
            return utcTimestamper?.GetFormattedTimestamp();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MyAndroid.MyServices
{
    /// <summary>
    /// 服务连接实现
    /// </summary>
    public class ServiceConnectionImpl : Java.Lang.Object, IServiceConnection, IGetTimestamp
    {
        private const string TAG = "MyDebug";
        TestServiceActivity mainActivity;
        /// <summary>
        /// 已连接
        /// </summary>
        public Boolean IsConnected { get; private set; }
        /// <summary>
        /// 服务绑定
        /// </summary>
        public ServiceBinder Binder { get; private set; }

        public ServiceConnectionImpl(TestServiceActivity activity)
        {
            IsConnected = false;
            Binder = null;
            mainActivity = activity;
        }

        /// <summary>
        /// 当服务连接时
        /// </summary>
        /// <param name="name"></param>
        /// <param name="service"></param>
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as ServiceBinder;
            IsConnected = this.Binder != null;

            string message = "onServiceConnected - ";
            Log.Debug(TAG, $"OnServiceConnected {name.ClassName}");

            if (IsConnected)
            {
                message = message + " 绑定服务 " + name.ClassName;
                mainActivity.UpdateUiForBoundService();
            }
            else
            {
                message = message + " 没有绑定服务 " + name.ClassName;
                mainActivity.UpdateUiForUnboundService();
            }

            Log.Info(TAG, message);
        }

        /// <summary>
        /// 当服务断开时
        /// </summary>
        /// <param name="name"></param>
        public void OnServiceDisconnected(ComponentName name)
        {
            Log.Debug(TAG, $"OnServiceDisconnected {name.ClassName}");
            IsConnected = false;
            Binder = null;
        }

        public string GetFormattedTimestamp()
        {
            if (!IsConnected)
            {
                return null;
            }

            return Binder?.GetFormattedTimestamp();
        }
    }
}
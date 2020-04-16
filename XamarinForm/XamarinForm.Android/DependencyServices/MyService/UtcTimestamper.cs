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

namespace XamarinForm.Droid.DependencyServices.MyService
{
    public class UtcTimestamper : IGetTimestamp
    {
        DateTime startTime;

        public UtcTimestamper()
        {
            startTime = DateTime.UtcNow;
        }

        public string GetFormattedTimestamp()
        {
            TimeSpan duration = DateTime.UtcNow.Subtract(startTime);
            return $"服务于{startTime.ToString("yyyy-MM-dd HH:mm:ss")}启动，已运行 ({duration.Days}天{duration.Hours}小时{duration.Minutes}分钟{duration.Seconds}秒)";
        }
    }
}
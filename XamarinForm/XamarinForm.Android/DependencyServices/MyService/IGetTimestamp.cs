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
    public interface IGetTimestamp
    {
        /// <summary>
        /// 获取带格式的时间
        /// </summary>
        /// <returns></returns>
        string GetFormattedTimestamp();
    }
}
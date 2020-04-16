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
    public class TimestampBinder : Binder, IGetTimestamp
    {
        public TimestampBinder(BackgroundService service)
        {
            this.Service = service;
        }

        public BackgroundService Service { get; private set; }

        public string GetFormattedTimestamp()
        {
            return Service?.GetFormattedTimestamp();
        }
    }
}
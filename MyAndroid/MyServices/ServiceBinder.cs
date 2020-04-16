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

namespace MyAndroid.MyServices
{
    public class ServiceBinder : Binder, IGetTimestamp
    {
        public ServiceBinder(DemoService service)
        {
            Service = service;
        }

        public DemoService Service { get; private set; }

        public string GetFormattedTimestamp()
        {
            return Service?.GetFormattedTimestamp();
        }
    }
}
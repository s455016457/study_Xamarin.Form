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

namespace XamarinForm.Droid
{
    [Activity(Label = "Test Mobile App", Icon = "@drawable/icon", MainLauncher = false)]
    public class WellCome : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WellCome);
            // Create your application here
        }
    }
}
using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.Util;

namespace XamarinForm.Droid
{
    [Activity(Label = "XamarinForm", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(bundle);

#if !DEBUG
            UserDialogs.Init(this); //Debug状态注册失败
#endif
                global::Xamarin.Forms.Forms.Init(this, bundle);
                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                Log.Debug("myDebug", ex.Message);
            }
        }
    }
}


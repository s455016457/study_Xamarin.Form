using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace MyAndroid
{
    [Activity(Label = "Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button testNotificationsButton, testServiceButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            testNotificationsButton = FindViewById<Button>(Resource.Id.test_notifications);
            testServiceButton = FindViewById<Button>(Resource.Id.test_service);

            testNotificationsButton.Click += TestNotificationsButton_Click;
            testServiceButton.Click += TestServiceButton_Click;
        }

        private void TestServiceButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                var intent = new Intent(this, typeof(TestServiceActivity));
                StartActivity(intent);
            }
            catch (Exception ex)
            {

            }
        }

        private void TestNotificationsButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                var intent = new Intent(Application.Context, typeof(TestNotificationActivity));
                StartActivity(intent);
            }
            catch (Exception ex)
            {

            }
        }
    }
}


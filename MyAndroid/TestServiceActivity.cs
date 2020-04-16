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
using MyAndroid.MyServices;

namespace MyAndroid
{
    [Activity(Label = "TestServiceActivity")]
    public class TestServiceActivity : Activity
    {
        Button GetTimestamperbutton, UNBinderButton, BinderButton;
        TextView messageText;

        ServiceConnectionImpl serviceConnection;
        Intent serviceToStart;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TestService);

            messageText = FindViewById<TextView>(Resource.Id.messageText);
            GetTimestamperbutton = FindViewById<Button>(Resource.Id.GetTimestamperbutton);
            UNBinderButton = FindViewById<Button>(Resource.Id.UNBinderButton);
            BinderButton = FindViewById<Button>(Resource.Id.BinderButton);

            GetTimestamperbutton.Click += GetTimestampButton_Click;
            UNBinderButton.Click += StopServiceButton_Click;
            BinderButton.Click += RestartServiceButton_Click;
        }
        protected override void OnStart()
        {
            base.OnStart();
            if (serviceConnection == null)
            {
                serviceConnection = new ServiceConnectionImpl(this);
            }
            DoBindService();
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (serviceConnection.IsConnected)
            {
                UpdateUiForBoundService();
            }
            else
            {
                UpdateUiForUnboundService();
            }
        }
        protected override void OnPause()
        {
            GetTimestamperbutton.Click -= GetTimestampButton_Click;
            UNBinderButton.Click -= StopServiceButton_Click;
            BinderButton.Click -= RestartServiceButton_Click;

            base.OnPause();
        }

        protected override void OnStop()
        {
            DoUnBindService();
            base.OnStop();
        }

        void DoBindService()
        {
            try
            {
                serviceToStart = new Intent(this, typeof(DemoService));
                BindService(serviceToStart, serviceConnection, Bind.AutoCreate);
                StartService(serviceToStart);
                messageText.Text = "";
            }
            catch (Exception ex)
            {
                Log.Debug("MyDebug", ex.Message);
            }
        }

        void DoUnBindService()
        {
            UnbindService(serviceConnection);
            StopService(serviceToStart);
            BinderButton.Enabled = true;
            messageText.Text = "";
        }

        internal void UpdateUiForBoundService()
        {
            GetTimestamperbutton.Enabled = true;
            UNBinderButton.Enabled = true;
            BinderButton.Enabled = false;

        }
        internal void UpdateUiForUnboundService()
        {
            GetTimestamperbutton.Enabled = false;
            UNBinderButton.Enabled = false;
            BinderButton.Enabled = true;
        }
        void GetTimestampButton_Click(object sender, System.EventArgs e)
        {
            if (serviceConnection.IsConnected)
            {
                messageText.Text = serviceConnection.Binder.Service.GetFormattedTimestamp();
            }
            else
            {
                messageText.SetText(Resource.String.service_not_connected);
            }
        }

        void StopServiceButton_Click(object sender, System.EventArgs e)
        {
            DoUnBindService();
            UpdateUiForUnboundService();


        }

        void RestartServiceButton_Click(object sender, System.EventArgs e)
        {
            DoBindService();
            UpdateUiForBoundService();
        }
    }
}
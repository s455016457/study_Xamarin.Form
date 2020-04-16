using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;
using XamarinForm.DependencyServices;

namespace XamarinForm.Pages.DependencyServices
{
    public class TestBackgroundServicePage:ContentPage
    {
        Label messageLabel;
        int MessageIndex = 0;
        public TestBackgroundServicePage()
        {
            messageLabel = new Label();
            Content = new StackLayout
            {
                Margin = new Thickness(10),
                Children = {
                    messageLabel,
                    new Button{
                        Text="启动服务",
                        Command=new Command(StartService),
                    },
                    new Button{
                        Text="停止服务",
                        Command=new Command(StopService),
                    },
                    new Button{
                        Text="清除所有消息",
                        Command=new Command(ClearNotifications),
                    },
                },
            };
        }
        
        private void StartService()
        {
            messageLabel.Text = "服务开启";
            App.BackgroundService.Start();
        }

        private void StopService()
        {
            messageLabel.Text = "服务停止";
            App.BackgroundService.Stop();
        }

        private void ClearNotifications()
        {
            App.MyNotificationService.Clear();
        }
    }
}

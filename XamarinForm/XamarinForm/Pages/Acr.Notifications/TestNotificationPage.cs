using Plugin.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Acr.Notifications
{
    public class TestNotificationPage : ContentPage
    {
        int id = 100;
        Entry titleEntry, messageEntry;

        Switch delayedSendSwitch;
        Picker delayedPicker;
        public TestNotificationPage()
        {
            Title = "销售推送 ACR Notifications";

            titleEntry = new Entry
            {
                Placeholder = "消息标题",
                Text="这里是通知标题",
            };

            messageEntry = new Entry
            {
                Placeholder = "消息正文",
                Text= "这里是通知正文。。。。。",
            };

            delayedSendSwitch = new Switch
            {
                IsToggled = false,
            };

            delayedSendSwitch.Toggled += DelayedSend_Toggled;

            delayedPicker = new Picker
            {
                Items = { "5", "10", "15" },
                IsVisible = false,
            };
            var btnPermission = new Button { Text = "请求许可" };

            Button sendButton = new Button
            {
                Text = "发送通知",
                Command = new Command(SendButtonCommand),
            };
            Button sendButton1 = new Button
            {
                Text = "发送自定义通知通知",
                Command = new Command(SendButton1Command),
            };

            Button clearButton = new Button
            {
                Text = "清除通知",
            };

            btnPermission.Command= new Command(async () =>
            {
                var result = await CrossNotifications.Current.RequestPermission();
                btnPermission.Text = result ? "Permission Granted 授权" : "Permission Denied 拒绝";
            });   

            clearButton.Clicked += ClearButton_Clicked
                ;
            Content = new StackLayout
            {
                Margin = new Thickness(10),
                Children = {
                    titleEntry,
                    messageEntry,
                    new StackLayout{
                        Orientation=StackOrientation.Horizontal,
                        Children={
                            delayedSendSwitch,
                            new Label{ Text="延迟推送"},
                        },
                    },
                    delayedPicker,
                    btnPermission,
                    sendButton,
                    sendButton1,
                    clearButton,
                },
            };
        }

        private async void ClearButton_Clicked(object sender, EventArgs e)
        {
            await CrossNotifications.Current.CancelAll();
        }

        private async void SendButtonCommand()
        {
            var notification = new Notification
            {
                Vibrate = true,
                Title = titleEntry.Text,
                Message = messageEntry.Text,
                //Date = DateTime.Now
            };

            if (delayedSendSwitch.IsToggled && delayedPicker.SelectedItem != null && !String.IsNullOrEmpty(delayedPicker.SelectedItem.ToString()))
            {
                notification.When = TimeSpan.FromSeconds(double.Parse(delayedPicker.SelectedItem.ToString()));
            }
            await CrossNotifications.Current.SetBadge(new Random().Next(100));
            await CrossNotifications.Current.Send(notification);
        }

        private async void SendButton1Command()
        {
            id++;
            var notification = new XamarinForm.DependencyServices.Notification
            {
                Vibrate = true,
                Title = titleEntry.Text+id,
                Message = messageEntry.Text+id,
                //Date = DateTime.Now
            };

            if (delayedSendSwitch.IsToggled && delayedPicker.SelectedItem != null && !String.IsNullOrEmpty(delayedPicker.SelectedItem.ToString()))
            {
                notification.When = TimeSpan.FromSeconds(double.Parse(delayedPicker.SelectedItem.ToString()));
            }
            await CrossNotifications.Current.SetBadge(new Random().Next(100));
            await App.NotificationService.Send(notification);
        }

        private void DelayedSend_Toggled(object sender, ToggledEventArgs e)
        {
            if (delayedPicker.IsVisible)
                delayedPicker.IsVisible = false;
            else
                delayedPicker.IsVisible = true;
        }
    }
}

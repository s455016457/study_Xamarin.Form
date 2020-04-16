using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForm.Pages.Acr.Notifications
{
    public class TestMyNotificationPage : ContentPage
    {
        Entry idEntry,titleEntry, subTitleEntry, messageEntry;
        public TestMyNotificationPage()
        {
            idEntry = new Entry
            {
                Text = "这里是通知ID",
                Placeholder = "1",
                PlaceholderColor = Color.Gray,
                Keyboard=Keyboard.Numeric,
            };
            titleEntry = new Entry
            {
                Text="这里是通知标题",
                Placeholder="通知标题",
                PlaceholderColor=Color.Gray,
            };

            subTitleEntry = new Entry
            {
                Text= "这里是通知副标题",
                Placeholder = "通知副标题",
                PlaceholderColor = Color.Gray,
            };

            messageEntry = new Entry
            {
                Text = "这里是通知正文",
                Placeholder = "通知正文",
                PlaceholderColor = Color.Gray,
            };

            Button SendButton = new Button
            {
                Text = "发送通知",
                Command=new Command(SendNotification),
            };

            Content = new StackLayout {
                Children = {
                    idEntry,
                    titleEntry,
                    subTitleEntry,
                    messageEntry,
                    SendButton
                },
                Margin=new Thickness(5),
            };
        }

        private async void SendNotification()
        {
            XamarinForm.DependencyServices.MyNotification.NotificationConfig notification = new XamarinForm.DependencyServices.MyNotification.NotificationConfig
            {
                Title=titleEntry.Text,
                Subtitle=subTitleEntry.Text,
                Badge=1,
                Id=idEntry.Text.Length==0?1:int.Parse(idEntry.Text),
                IsSound=true,
                IsVibrate=true,
                Message=messageEntry.Text,
            };
            await App.MyNotificationService.Send(notification);
        }
    }
}

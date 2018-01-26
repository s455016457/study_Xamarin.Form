using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Delegates;

namespace XamarinForm.Views
{
    public class LoginView : ContentView
    {
        Entry userName, passwrod;
        Button loginButtong;
        public LoginViewLoginButtonClickHandle OnLoginButtonClick { get; set; }

        public LoginView()
        {
            StackLayout stackLayout = new StackLayout
            {
                BackgroundColor = Color.WhiteSmoke,
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10, 10, 10, 10)
            };

            Label label = new Label
            {
                BackgroundColor=Color.DarkGray,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions =LayoutOptions.Fill,
                Margin = new Thickness(0, 5, 0, 5),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = "登录"
            };
            
            userName = new Entry
            {
                Keyboard = Keyboard.Default,
                Placeholder = "请输入登录账号",
                Margin=new Thickness(0,5,0,5),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry)),
                PlaceholderColor =Color.Gray
            };

            passwrod = new Entry
            {
                IsPassword = true,
                Keyboard = Keyboard.Default,
                Placeholder = "请输入密码",
                Margin = new Thickness(0, 5, 0, 5),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry)),
                PlaceholderColor = Color.Gray
            };

            loginButtong = new Button
            {
                Text="登录",
                Margin = new Thickness(0, 5, 0, 5),
                HorizontalOptions =LayoutOptions.Center,
            };
            loginButtong.Clicked += OnLoginButtonClicked;

            stackLayout.Children.Add(label);
            stackLayout.Children.Add(userName);
            stackLayout.Children.Add(passwrod);
            stackLayout.Children.Add(loginButtong);

            Content = stackLayout;
        }
        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (OnLoginButtonClick != null)
            {
                OnLoginButtonClick.Invoke(userName.Text, passwrod.Text);
            }
        }
    }
}

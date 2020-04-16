using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.WebApiService;

namespace XamarinForm.Pages.HttpReque
{
    public class TestHttpClientPage: ContentPage
    {
        Label label;
        Entry ipEntry;
        User user = new User
        {
            Uid = "",
            Name = "初始Name",
            Pwd = "初始Pwd",
        };
        public TestHttpClientPage()
        {
            BindingContext = user;
            Button button = new Button
            {
                Text = "获取服务器数据",
            };

            Button button1 = new Button
            {
                Text = "修改数据",
            };

            Button button2 = new Button
            {
                Text = "异步修改数据",
            };

            label = new Label();
            label.SetBinding(Label.TextProperty, "Uid");
            ipEntry = new Entry
            {
                Placeholder = "请输入IP地址",
                Text =ClientConfig.ServiceAddress,
                PlaceholderColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry))
            };

            button.Clicked += Button_Clicked;
            button1.Clicked += Button1_Clicked;
            button2.Clicked += Button2_Clicked;

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                    ipEntry, button1,button2,button,label,
                }
            };
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            user.Uid = "修改数据";
        }
        private async void Button2_Clicked(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                user.Uid = "异步修改数据";
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            user.Uid = "开始加载服务数据";
            RequestParamater paramater = new RequestParamater();
            paramater.Add("UID", "这里是用户名");
            paramater.Add("PASSWORD", "这里是密码");

            string ip = ipEntry.Text;
            ClientConfig.SetServiceAddress(ip);
            if (String.IsNullOrEmpty(ip))
            {
                await DisplayAlert("温馨提示", "请输入IP地址", "确定");
                return;
            }

            String uri = String.Format("http://{0}/api/Login", ip);

            WebApiClient webApiClient = new WebApiClient();
            System.Net.Http.HttpContent httpContent = await webApiClient.PostAsync("Login", paramater);

            String str = await httpContent.ReadAsStringAsync();

            user.Uid += str;
        }
    }
}

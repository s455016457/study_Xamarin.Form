using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.WebApiService;

namespace XamarinForm.Pages.HttpReque
{
	public class TestPage : ContentPage
	{
        Label label;
        Entry ipEntry;
		public TestPage ()
		{
            Button button = new Button
            {
                Text = "获取数据",
            };

            label = new Label();
            ipEntry = new Entry
            {
                Placeholder = "请输入IP地址",
                Text="192.168.1.115",
                PlaceholderColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry))
            };

            button.Clicked += Button_Clicked;
			Content = new StackLayout {
                Orientation=StackOrientation.Vertical,
				Children = {
                    ipEntry, button,label,
                }
			};
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            RequestParamater paramater=new RequestParamater();
            paramater.Add("Name", "这里是用户名");
            paramater.Add("Pwd", "这里是密码");

            string ip = ipEntry.Text;
            if (String.IsNullOrEmpty(ip))
            {
                await DisplayAlert("温馨提示", "请输入IP地址", "确定");
                return;
            }

            String uri = String.Format("http://{0}:8082/api/Test", ip);

            User user = await HttpClient.GetObjectByGetAsync<User>(uri, paramater, p =>
            {
                p.Uid += "CallBack添加数据";
            });
            if (user != null)
            {
                label.Text = String.Format("Uid:{0},Name:{1},Pwd:{2}", user.Uid,user.Name,user.Pwd);
            }
        }
    }
    public class User
    {
        public String Uid { get; set; }
        public String Name { get; set; }
        public String Pwd { get; set; }
    }
}
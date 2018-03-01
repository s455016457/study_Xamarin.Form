using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.ViewModel;
using XamarinForm.WebApiService;

namespace XamarinForm.Pages.HttpReque
{
	public class TestPage : ContentPage
	{
        Label label;
        Entry ipEntry;
        User user = new User {
            Uid="初始UID",
            Name="初始Name",
            Pwd="初始Pwd",
        };
        public TestPage ()
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
                Text="192.168.1.115:8082",
                PlaceholderColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry))
            };

            button.Clicked += Button_Clicked;
            button1.Clicked += Button1_Clicked;
            button2.Clicked += Button2_Clicked;

            Content = new StackLayout {
                Orientation=StackOrientation.Vertical,
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
            RequestParamater paramater=new RequestParamater();
            paramater.Add("Name", "这里是用户名");
            paramater.Add("Pwd", "这里是密码");

            string ip = ipEntry.Text;
            if (String.IsNullOrEmpty(ip))
            {
                await DisplayAlert("温馨提示", "请输入IP地址", "确定");
                return;
            }

            String uri = String.Format("http://{0}/api/Test", ip);

            await HttpClient.GetObjectByGetAsync<User>(uri, paramater, p =>
            {
                p.Uid += "CallBack添加数据";
                user.Uid = p.Uid;
            });
            //if (user != null)
            //{
            //    label.Text = String.Format("Uid:{0},Name:{1},Pwd:{2}", user.Uid,user.Name,user.Pwd);
            //}
        }
    }
    public class User: BaseViewModel
    {
        String _uid, _name, _pwd;
        public String Uid
        {
            get { return _uid; }
            set
            {
                if (_uid != value)
                {
                    _uid = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Pwd
        {
            get { return _pwd; }
            set
            {
                if (_pwd != value)
                {
                    _pwd = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
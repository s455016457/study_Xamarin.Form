using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.Extensions;
using XamarinForm.ViewModel;
using XamarinForm.WebApiService;

namespace XamarinForm.Pages.HttpReque
{
    public class TestPage : ContentPage
    {
        const String APPKEY = "904EG960DV56ISV5FI0K51X960UW5ZJT", APPSECRET = "G6TL12YIL1VX6A1UM526JM15YJB1W26B";

        Label label;
        Entry ipEntry;
        User user = new User
        {
            Uid = "初始UID",
            Name = "初始Name",
            Pwd = "初始Pwd",
        };
        public TestPage()
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


            Button button3 = new Button
            {
                Text = "获取服务器数据_SWERP_Api",
            };

            label = new Label();
            label.SetBinding(Label.TextProperty, "Uid");
            ipEntry = new Entry
            {
                Placeholder = "请输入IP地址",
                Text = "192.168.1.108:8082",
                PlaceholderColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry))
            };

            button.Clicked += Button_Clicked;
            button1.Clicked += Button1_Clicked;
            button2.Clicked += Button2_Clicked;
            button3.Clicked += Button3_Clicked;

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                    ipEntry, button1,button2,button,button3,label,
                }
            };
        }

        private void Button3_Clicked(object sender, EventArgs e)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.108:8083/api/Login");
            request.ContentType = "application/json";
            request.Headers.Add("api_version", "1.1");
            request.Method = "Post";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8");
            request.Timeout = 20 * 1000;

            //这个在Post的时候，一定要加上，如果服务器返回错误，他还会继续再去请求，不会使用之前的错误数据，做返回数据  
            request.ServicePoint.Expect100Continue = false;

            RequestParamater requestParamater = new RequestParamater
            {
                { "Uid", "admin" },
                { "Password", "123456" }
            };

            String jsonParam = requestParamater.ToJson();

            RequestAddSignature(request.Headers, jsonParam);
            jsonParam = jsonParam.EncryptDES(APPSECRET);

            byte[] bytes = Encoding.UTF8.GetBytes(jsonParam);
            request.ContentLength = bytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Flush();
            requestStream.Close();

            var response = request.GetResponse();
            StringBuilder sb = new StringBuilder();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    String line = String.Empty;

                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line);
                        Console.WriteLine(String.Format("获取响应数据：{0}", line));
                    }
                }
            }

            String strValue = sb.ToString();
            Console.WriteLine(strValue);
            strValue = strValue.DecryptDES(APPSECRET);
            if (strValue.Length>0)
            {
                user.Uid = String.Format("{0} {1} 服务器加载数据",user.Uid, strValue );
            }
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
            if (String.IsNullOrEmpty(ip))
            {
                await DisplayAlert("温馨提示", "请输入IP地址", "确定");
                return;
            }

            //String uri = String.Format("http://{0}/api/Test", ip);

            String uri = "http://192.168.1.108:8082/api/Test";

            //获得的对象是新的，直接复制给user将导致SetBinding失效，需要用返回的对象修改user中的属性值
            await HttpClient.GetObjectByPostAsync<User>(uri, paramater, p =>
            {
                p.Uid += "CallBack添加数据";
                user.Uid = p.Uid;
            });
            if (user != null)
            {
                user.Uid += "CallBack之后添加数据";
            }
        }

        /// <summary>
        /// 给请求添加签名
        /// </summary>
        /// <param name="header"></param>
        /// <param name="param"></param>
        private static void RequestAddSignature(WebHeaderCollection header, String param)
        {
            String timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");

            string Signature = GetSignature(APPKEY, APPSECRET, timestamp, param);
            header.Remove("app_key");
            header.Remove("timestamp");
            header.Remove("sign");
            header.Add("app_key", APPKEY); ;
            header.Add("timestamp", timestamp);
            header.Add("sign", Signature);
        }

        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="secret">签名字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="appparam">请求参数</param>
        /// <returns></returns>
        private static String GetSignature(String appkey, String secret, String timestamp, String appparam)
        {
            var hash = System.Security.Cryptography.MD5.Create();

            Dictionary<String, String> dic = new Dictionary<string, string>
            {
                { "param_json", appparam },
                { "app_key", appkey },
                { "timestamp", timestamp }
            };

            // 第一步：把字典按Key的字母顺序排序
            IEnumerator<KeyValuePair<String, String>> dem = dic.OrderBy(p => p.Key).GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder(secret);
            while (dem.MoveNext())
            {
                String key = dem.Current.Key;
                String value = dem.Current.Value;
                if (!String.IsNullOrWhiteSpace(key) && !String.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }
            query.Append(secret);

            // 第三步：使用MD5加密
            MD5 md5 = MD5.Create();
            Byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));

            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (Int32 i = 0; i < bytes.Length; i++)
            {
                String hex = bytes[i].ToString("X2");
                result.Append(hex);
            }

            return result.ToString();
        }

    }

    public class User : BaseViewModel
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
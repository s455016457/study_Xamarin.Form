using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.Models;
using XamarinForm.Pages.Control;
using XamarinForm.Services;
using XamarinForm.Utilities;

namespace XamarinForm
{
	public partial class App : Application
	{
        public static LoginBill LoginBill { get; private set; }
        /// <summary>
        /// 用户已登录
        /// </summary>
        public static bool IsUserLoggedIn { get; set; }
        
        public App ()//每次打开应用程序都会执行该方法，无论是第一次打开还是Sleep后打开
		{
			InitializeComponent();
            //MainPage = new XamarinForm.MainPage();
            //IList<ToolbarItem> toolbarItem = new List<ToolbarItem>();
            // MainPage = new NavigationPage(new MyMasterDetailPage()) /*{ BarBackgroundColor = Color.Green, BarTextColor = Color.Blue,Title="示例APP" }*/;
            RecoverLoginBill();

            IsUserLoggedIn = LoginService.VerificationBill(LoginBill);

            //MainPage = new NavigationPage(new MyMasterDetailPage());
            MainPage = new MyMasterDetailPage2();


            //MainPage = new TestPDFWebViewPage();
            //if (IsUserLoggedIn)
            //{
            //    MainPage = new NavigationPage(new MyMasterDetailPage());
            //    //MainPage = new MyMasterDetailPage();
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new LoginPage());
            //    //MainPage = new MyMasterDetailPage();
            //}            
        }

        /// <summary>
        /// 设置登录凭证
        /// </summary>
        /// <param name="loginBill"></param>
        public static void SetLoginBill(LoginBill loginBill)
        {
            LoginBill = loginBill;
        }

        public void UpdateMainPage(Page page)
        {
            MainPage = page;
        }

        protected override void OnStart()
        {
            RecoverLoginBill();
        }

		protected override void OnSleep ()
		{
            if (LoginBill == null)
            {
                if (Current.Properties.ContainsKey(AppConstant.LoginUserName))
                    Current.Properties.Remove(AppConstant.LoginUserName);

                if (Current.Properties.ContainsKey(AppConstant.LoginPassword))
                    Current.Properties.Remove(AppConstant.LoginPassword);

                if (Current.Properties.ContainsKey(AppConstant.LoginBillTime))
                    Current.Properties.Remove(AppConstant.LoginBillTime);
            }
            else
            {
                if (Current.Properties.ContainsKey(AppConstant.LoginUserName))
                    Current.Properties[AppConstant.LoginUserName] = LoginBill.UserName;
                else
                    Current.Properties.Add(AppConstant.LoginUserName, LoginBill.UserName);

                if (Current.Properties.ContainsKey(AppConstant.LoginPassword))
                    Current.Properties[AppConstant.LoginPassword] = LoginBill.Password;
                else
                    Current.Properties.Add(AppConstant.LoginPassword, LoginBill.Password);

                if (Current.Properties.ContainsKey(AppConstant.LoginBillTime))
                    Current.Properties[AppConstant.LoginBillTime] = LoginBill.BillTime.ToString();
                else
                    Current.Properties.Add(AppConstant.LoginBillTime, LoginBill.BillTime.ToString());
            }
        }

		protected override void OnResume ()
        {
            RecoverLoginBill();
        }

        private void RecoverLoginBill()
        {
            if (LoginBill == null)
            {
                String UserName = String.Empty, Password = String.Empty, StrBillTime = String.Empty;
                DateTime BillTime = DateTime.Now;

                if (Current.Properties.ContainsKey(AppConstant.LoginUserName))
                    UserName = Current.Properties[AppConstant.LoginUserName].ToString();

                if (Current.Properties.ContainsKey(AppConstant.LoginPassword))
                    Password = Current.Properties[AppConstant.LoginPassword].ToString();

                if (Current.Properties.ContainsKey(AppConstant.LoginBillTime))
                    StrBillTime = Current.Properties[AppConstant.LoginBillTime].ToString();

                if (DateTime.TryParse(StrBillTime, out BillTime))
                {
                    LoginBill = LoginBill.Recover(UserName, Password, BillTime);
                }
            }
        }

    }
}

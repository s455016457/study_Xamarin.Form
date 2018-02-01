using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.Models;
using XamarinForm.Services;
using XamarinForm.Utilities;
using XamarinForm.Views;

namespace XamarinForm
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            Title = "登录";

            if (App.Current.Properties.ContainsKey(AppConstant.LoginUserName))
                App.Current.Properties.Remove(AppConstant.LoginUserName);

            if (App.Current.Properties.ContainsKey(AppConstant.LoginPassword))
                App.Current.Properties.Remove(AppConstant.LoginPassword);

            if (App.Current.Properties.ContainsKey(AppConstant.LoginBillTime))
                App.Current.Properties.Remove(AppConstant.LoginBillTime);
            
            LoginView loginView = new LoginView();
            loginView.OnLoginButtonClick += OnLoginButtonClick;
            Content = loginView;
        }
        void OnLoginButtonClick(String UserName, String Passwrod)
        {
            if (LoginBill.CurrentBill != null)
            {
                LoginBill.CurrentBill.Dispose();
            }

            LoginBill loginBill = LoginBill.Create(UserName, Passwrod);
            if (LoginService.VerificationBill(loginBill))
            {
                if (App.Current.Properties.ContainsKey(AppConstant.LoginUserName))
                    App.Current.Properties[AppConstant.LoginUserName] = loginBill.UserName;
                else
                    App.Current.Properties.Add(AppConstant.LoginUserName, loginBill.UserName);

                if (App.Current.Properties.ContainsKey(AppConstant.LoginPassword))
                    App.Current.Properties[AppConstant.LoginPassword] = loginBill.Password;
                else
                    App.Current.Properties.Add(AppConstant.LoginPassword, loginBill.Password);

                if (App.Current.Properties.ContainsKey(AppConstant.LoginBillTime))
                    App.Current.Properties[AppConstant.LoginBillTime] = loginBill.BillTime.ToString();
                else
                    App.Current.Properties.Add(AppConstant.LoginBillTime, loginBill.BillTime.ToString());
                
                App.SetLoginBill(loginBill);
                App.IsUserLoggedIn = true;

                //MyMasterDetailPage parentPage = Parent as MyMasterDetailPage;
                //if (parentPage != null)
                //    parentPage.Detail = parentPage.GetCurrentPage();


                NavigationPage navigation = Parent as NavigationPage;
                if (navigation != null)
                {
                    MyMasterDetailPage2 parentPage = navigation.Parent as MyMasterDetailPage2;
                    if (parentPage != null)
                        parentPage.Detail = new NavigationPage(parentPage.GetCurrentPage());
                }
            }
            else
            {
                loginBill.Dispose();
            }
        }
    }
}
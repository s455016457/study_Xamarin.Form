using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Utilities;
using System.Linq;

namespace XamarinForm
{
    public class MyMasterDetailPage : MasterDetailPage
    {
        ListMenuAnimatePage2 listMenuPage;
        public MyMasterDetailPage()
        {
            ToolbarItems.Add(new ToolbarItem("关于我们", "", () => { }) { Order = ToolbarItemOrder.Secondary });
            ToolbarItems.Add(new ToolbarItem("设置", "", () => { }) { Order = ToolbarItemOrder.Secondary });
            ToolbarItems.Add(new ToolbarItem("注销", "", logout) { Order = ToolbarItemOrder.Secondary });
            ToolbarItems.Add(new ToolbarItem("退出", "", () => { }) { Order = ToolbarItemOrder.Secondary });
            Title = "示例APP";
            listMenuPage = new ListMenuAnimatePage2();
            Master = listMenuPage;

            Detail = GetCurrentPage();
            //if (!App.IsUserLoggedIn)
            //{
            //    Detail =new LoginPage();
            //}
            //else
            //{
            //    if (Application.Current.Properties.ContainsKey(AppConstant.CurrentPage))
            //    {
            //        object MenuItemId = Application.Current.Properties[AppConstant.CurrentPage];
            //        if (MenuItemId != null)
            //        {
            //            //Detail = new NavigationPage(listMenuPage.GetPage(MenuItemId.ToString()));
            //            Detail = listMenuPage.GetPage(MenuItemId.ToString());
            //        }
            //        else
            //        {
            //            //Detail = new NavigationPage(new WelcomePage());
            //            Detail = new WelcomePage();
            //        }
            //    }
            //    else
            //    {
            //        //Detail = new NavigationPage(new WelcomePage());
            //        Detail = new WelcomePage();
            //    }
            //}

            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                //to do something
            }
            listMenuPage.OnListMenuItemClick += p =>
            {
                Title = p.Title;
                if (!App.IsUserLoggedIn)
                {
                    Detail = new LoginPage();
                }
                else
                {
                    Detail = listMenuPage.GetPage(p.MenuItemId);
                }
                IsPresented = false;
                Application.Current.Properties[AppConstant.CurrentPage] = p.MenuItemId;
                return false;
            };

            UpdateMasterBehavior();
        }

        public Page GetCurrentPage()
        {
            if (!App.IsUserLoggedIn)
                return new LoginPage();
            if (Application.Current.Properties.ContainsKey(AppConstant.CurrentPage))
            {
                object MenuItemId = Application.Current.Properties[AppConstant.CurrentPage];
                if (MenuItemId != null)
                    //Detail = new NavigationPage(listMenuPage.GetPage(MenuItemId.ToString()));
                    return listMenuPage.GetPage(MenuItemId.ToString());
            }
            //Detail = new NavigationPage(new WelcomePage());
            return new WelcomePage();
        }

        public void logout()
        {
            App.IsUserLoggedIn = false;
            App.LoginBill.Dispose();
            App.SetLoginBill(null);

            Detail = GetCurrentPage();
            //Navigation.InsertPageBefore(new LoginPage(), this);
            //await Navigation.PopAsync(true);
            //App app = App.Current as App;
            //app.UpdateMainPage(new NavigationPage(new LoginPage()));

            //App.navigationPage = new NavigationPage(new LoginPage());
            //await App.navigationPage.PopToRootAsync();

            //App.Current.MainPage = new NavigationPage(new LoginPage());

            //App.Current.MainPage = new NavigationPage();
            //await ((NavigationPage)App.Current.MainPage).PushAsync(new LoginPage(), true);
        }
    }
}
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
                    return listMenuPage.GetPage(MenuItemId.ToString());
            }
            return new WelcomePage();
        }

        public void logout()
        {
            App.IsUserLoggedIn = false;
            App.LoginBill.Dispose();
            App.SetLoginBill(null);

            Detail = GetCurrentPage();
        }
    }
}
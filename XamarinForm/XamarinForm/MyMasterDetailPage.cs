using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Utilities;
using System.Linq;

namespace XamarinForm
{
    public class MyMasterDetailPage:MasterDetailPage
    {
        ListMenuAnimatePage2 listMenuPage;
        public MyMasterDetailPage()
        {
            ToolbarItems.Add(new ToolbarItem("关于我们", "", () => { }) { Order = ToolbarItemOrder.Secondary });
            ToolbarItems.Add(new ToolbarItem("设置", "", () => { }) { Order = ToolbarItemOrder.Secondary });
            ToolbarItems.Add(new ToolbarItem("注销", "", () => { }) { Order = ToolbarItemOrder.Secondary });
            ToolbarItems.Add(new ToolbarItem("退出", "", () => { }) { Order = ToolbarItemOrder.Secondary });
            Title = "示例APP";
            listMenuPage = new ListMenuAnimatePage2();
            Master = listMenuPage;

            if (Application.Current.Properties.ContainsKey(AppConstant.CurrentPage))
            {
                object MenuItemId = Application.Current.Properties[AppConstant.CurrentPage];
                if (MenuItemId != null)
                    Detail = listMenuPage.GetPage(MenuItemId.ToString());
                else
                    Detail = new WelcomePage();
            }
            else
            {
                Detail = new WelcomePage();
            }

            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                //to do something
            }
            listMenuPage.OnListMenuItemClick += p =>
            {                
                Title = p.Title;
                Detail = listMenuPage.GetPage(p.MenuItemId);
                IsPresented = false;
                Application.Current.Properties[AppConstant.CurrentPage] = p.MenuItemId;
                return false;
            };
        }
    }
}

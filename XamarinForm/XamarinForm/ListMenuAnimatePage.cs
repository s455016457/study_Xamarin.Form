using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Delegates;
using XamarinForm.Services;
using XamarinForm.Views;

namespace XamarinForm
{
    public class ListMenuAnimatePage : ContentPage
    {
        /// <summary>
        /// 菜单项被点击时触发
        /// </summary>
        public ListMenuItemClickHandle<Models.MenuItem> OnListMenuItemClick { get; set; }
        ListMenuDataStore listMenuData = new ListMenuDataStore();
        public ListMenuAnimatePage()
        {
            Title = "示例APP菜单";
            Content = ListMenu();
        }

        private View ListMenu()
        {
            ListMenuAnimate<Models.MenuItem> listMenu = new ListMenuAnimate<Models.MenuItem>(listMenuData.GetMenuItem());
            listMenu.OnListMenuItemClick = p =>
            {
                if (p.ChildrenMenu == null || p.ChildrenMenu.Count == 0)
                {
                    if (OnListMenuItemClick != null)
                        return OnListMenuItemClick.Invoke(p);
                    return false;
                }
                return true;
            };

            return listMenu;
        }

        public Page GetPage(String menuItemId)
        {
            return listMenuData.GetPage(menuItemId);
        }
    }
}
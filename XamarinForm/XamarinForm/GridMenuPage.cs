using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Views;

namespace XamarinForm
{
    public class GridMenuPage: ContentPage
    {
        public GridMenuPage()
        {
            Content = TestGridMenu();
            //BackgroundColor = Color.Blue;
            SizeChanged += MainPage_SizeChanged;
        }
        private View TestGridMenu()
        {
            IList<Models.MenuItem> list = new List<Models.MenuItem>();

            for (int i = 0; i < 29; i++)
            {
                list.Add(new Models.MenuItem("MenuItemId" + i, "Title" + i, ImageSource.FromFile("setting.png")));
                //list.Add(new Models.MenuItem("MenuItemId"+i, "Title"+i, "setting.png"));
            }
            GridMenu gridMenu = new GridMenu();
            gridMenu.ColumnDefinition = 4;
            gridMenu.BindData(list);
            return gridMenu;
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            GridMenu gridMenu = Content as GridMenu;
            if (gridMenu != null) gridMenu.Renderer();
            //ListMenu<Models.MenuItem> listMenu = Content as ListMenu<Models.MenuItem>;
            //if (listMenu != null) listMenu.Renderer();
        }
        
    }
}

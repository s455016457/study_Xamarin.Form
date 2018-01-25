using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.Views;

namespace XamarinForm
{
	public partial class MainPage_Test : ContentPage
	{
		public MainPage_Test()
		{
			InitializeComponent();
            
            Content = TestListMenu();
            //BackgroundColor = Color.Blue;
            //SizeChanged += MainPage_SizeChanged;


            //var button1 = new Button { Text = "TopAligned", VerticalOptions = LayoutOptions.StartAndExpand };
            //var button2 = new Button { Text = "CenterAligned", VerticalOptions = LayoutOptions.Center };
            //var button3 = new Button { Text = "BottomAligned", VerticalOptions = LayoutOptions.EndAndExpand };
            //var button4 = new Button { Text = "Fill", VerticalOptions = LayoutOptions.Fill };

            //StackLayout stack = new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal,
            //    Children = {
            //        button1,
            //        button2,
            //        button3,
            //        button4
            //     }
            //};

            //Content = stack;
        }

        private View TestListMenu()
        {
            Models.MenuItem menuItem = new Models.MenuItem("caidan", "菜单", ImageSource.FromFile("setting.png"));
            IList<Models.MenuItem> list = new List<Models.MenuItem>();
            menuItem.ChildrenMenu = list;

            for (int i = 0; i < 29; i++)
            {
                String MenuItemId = "MenuItemId_" + i;
                String Title = "Title" + i;
                IList<Models.MenuItem> childList = new List<Models.MenuItem>();
                var model = new Models.MenuItem(MenuItemId, Title, ImageSource.FromFile("setting.png"));
                model.ChildrenMenu = childList;
                model.ParentMenuItem = menuItem;
                list.Add(model);
                for (int j = 0; j < 15; j++)
                {
                    var childModel = new Models.MenuItem(MenuItemId + "_" + j, Title + "_" + j, ImageSource.FromFile("setting.png"));
                    childModel.ParentMenuItem = model;
                    childList.Add(childModel);
                }
            }

            ListMenu<Models.MenuItem> listMenu = new ListMenu<Models.MenuItem>(menuItem);
            listMenu.OnListMenuItemClick = p =>
            {
                if (p.ChildrenMenu == null || p.ChildrenMenu.Count == 0)
                {
                    NavigationPage navigationPage = App.Current.MainPage as NavigationPage;
                    navigationPage.PushAsync(new GridMenuPage(),false);
                    
                    return false;
                }
                return true;
            };

            return listMenu;
        }

        private View TestGridMenu()
        {
            IList<Models.MenuItem> list = new List<Models.MenuItem>();

            for (int i = 0; i < 29; i++)
            {
                list.Add(new Models.MenuItem("MenuItemId" + i, "Title" + i, ImageSource.FromFile("setting.png")));
                //list.Add(new Models.MenuItem("MenuItemId"+i, "Title"+i, "setting.png"));
            }
            Views.GridMenu gridMenu = new Views.GridMenu();
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

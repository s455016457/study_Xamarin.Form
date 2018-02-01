using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using XamarinForm.Views;
using XamarinForm.Delegates;
using XamarinForm.Services;
using XamarinForm.Utilities;
using System.Globalization;

namespace XamarinForm
{
    public class ListMenuAnimatePage3 : CarouselPage
    {
        /// <summary>
        /// 菜单项被点击时触发
        /// </summary>
        public ListMenuItemClickHandle<Models.MenuItem> OnListMenuItemClick { get; set; }
        ListMenuDataStore listMenuData = new ListMenuDataStore();
        Models.MenuItem menuItem;
        Boolean isAddPage = false, isBackClick = false;
        public ListMenuAnimatePage3()
        {

            Title = "示例APP菜单";
            menuItem = listMenuData.GetMenuItem();

            if (Application.Current.Properties.ContainsKey(AppConstant.MenuPath))
            {
                object MenuPath = Application.Current.Properties[AppConstant.MenuPath];
                if (MenuPath != null)
                {
                    String[] menuPaths = MenuPath.ToString().Split(AppConstant.MenuPathSeparator);
                    Boolean isFrist = true;
                    Models.MenuItem currentMenuItem = menuItem;
                    foreach (string menuItemId in menuPaths)
                    {
                        if (isFrist)
                        {
                            currentMenuItem = menuItem;
                            isFrist = false;
                        }
                        else
                        {
                            currentMenuItem = currentMenuItem.ChildrenMenu.FirstOrDefault(p => p.MenuItemId.Equals(menuItemId));
                        }
                        if (currentMenuItem != null)
                            AddPage(currentMenuItem);
                    }
                }
                else
                    AddPage(menuItem);
            }
            else

                AddPage(menuItem);

            MyMasterDetailPage2 myMaster = Parent as MyMasterDetailPage2;
            if (myMaster != null)
            {
               // CurrentPage.Content.Margin = new Thickness(0, 400, 0, 0);
            }
        }

        public void AddPage(Models.MenuItem menuItem)
        {
            isAddPage = true;
            MenuPage<Models.MenuItem> menuPage = new MenuPage<Models.MenuItem>(menuItem);
            menuPage.OnBackClick += backClick;
            menuPage.OnListMenuItemClick += ListMenuItemClick;

            Children.Add(menuPage);
            CurrentPage = Children.LastOrDefault();
        }

        void backClick()
        {
            isBackClick = true;
            if (Children.Count >= 1)
                Children.Remove(Children.LastOrDefault());
        }

        bool ListMenuItemClick(Models.MenuItem menuItem)
        {
            if (menuItem.ChildrenMenu == null || menuItem.ChildrenMenu.Count == 0)
            {
                if (menuItem.ParentMenuItem == null)
                {
                    Application.Current.Properties[AppConstant.MenuPath] = menuItem.ToMenuPath(AppConstant.MenuPathSeparator);
                }
                if (OnListMenuItemClick != null)
                    return OnListMenuItemClick.Invoke(menuItem);
                return false;
            }
            Application.Current.Properties[AppConstant.MenuPath] = menuItem.ToMenuPath(AppConstant.MenuPathSeparator);
            AddPage(menuItem);
            return true;            
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            if (!isAddPage && !isBackClick)
            {
                Children.Remove(Children.LastOrDefault());
            }
            isAddPage = isBackClick = false;
        }

        public Page GetPage(String menuItemId)
        {
            return listMenuData.GetPage(menuItemId);
        }

        class MenuPage<T> : ContentPage where T : Models.MenuItem
        {
            /// <summary>
            /// 菜单项被点击时触发
            /// </summary>
            public ListMenuItemClickHandle<Models.MenuItem> OnListMenuItemClick { get; set; }
            /// <summary>
            /// 返回按钮被点击时
            /// </summary>
            public Action OnBackClick { get; set; }

            public T MenuItem { get; private set; }

            private Label titleBackLabel;
            private Label titleLabel;
            private ListView listView;
            private StackLayout titlLayout;

            public MenuPage(T MenuItem)
            {
                //BindingContext = new ViewModel.ListMenuPageViewModel(MenuItem);
                this.MenuItem = MenuItem;
                initView();               
                initData();
            }

            void initView()
            {
                Grid grid = new Grid
                {
                    RowDefinitions = {
                        new RowDefinition{ Height=GridLength.Auto},
                        new RowDefinition{ Height=GridLength.Star },
                    },
                    ColumnDefinitions = {
                        new ColumnDefinition(),
                    },
                };

                titlLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Color.BlueViolet,
                    Padding = new Thickness(0, 5, 0, 5),
                };

                titleBackLabel = new Label
                {
                    BackgroundColor = Color.BlueViolet,
                    TextColor = Color.Blue,
                    Margin = new Thickness(7, 5, 5, 5),
                };
                titleBackLabel.FontSize = Device.GetNamedSize(NamedSize.Default, titleBackLabel);

                titleLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    BackgroundColor = Color.BlueViolet,
                    Margin = new Thickness(0, 5, 5, 5),
                };
                titleLabel.FontSize = Device.GetNamedSize(NamedSize.Default, titleLabel);
                titleLabel.FontAttributes = FontAttributes.Bold;

                var listMenuCell = new DataTemplate(typeof(ListMenuCell));
                listMenuCell.SetBinding(ListMenuCell.IconProperty, "Icon");
                listMenuCell.SetBinding(ListMenuCell.TitleProperty, "Title");
                listMenuCell.SetBinding(ListMenuCell.HasChildProperty, new Binding("ChildrenMenu", converter: new ReverseConverter()));

                listView = new ListView()
                {
                    //ItemTemplate = new DataTemplate(() => {
                    //    return new ListMenuItem<T>();
                    //})
                    Margin = new Thickness(5, 0, 5, 2),
                    ItemTemplate = listMenuCell
                };

                listView.ItemSelected += ListView_ItemSelected;

                titleBackLabel.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(clickBack)
                });


                titlLayout.Children.Add(titleBackLabel);
                titlLayout.Children.Add(titleLabel);

                grid.Children.Add(titlLayout, 0, 0);
                grid.Children.Add(listView, 0, 1);
                Content = grid;
            }

             void initData()
            {
                if (MenuItem.ParentMenuItem != null)
                {
                    titleBackLabel.IsEnabled = true;
                    titleBackLabel.Text = "<返回";
                }
                else
                {
                    titleBackLabel.IsEnabled = false;
                    titleBackLabel.Text = "";
                }
                titleLabel.Text = MenuItem.Title;
                listView.ItemsSource = MenuItem.ChildrenMenu;
                //titleLabel.SetBinding(Label.TextProperty, "Title");
                //listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "MenuItemList");
            }

            private void clickBack()
            {
                titleBackLabel.Opacity = 0.65;
                titleBackLabel.TextColor = Color.LightGray;

                Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
                {
                    titleBackLabel.Opacity = 1;
                    titleBackLabel.TextColor = Color.Blue;
                    if (OnBackClick != null) OnBackClick.Invoke();
                    return false;
                });
            }

            private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
            {
                Models.MenuItem _selectedItem = e.SelectedItem as Models.MenuItem;
                if (_selectedItem == null)
                {
                    return;
                }
                if (OnListMenuItemClick != null)
                {
                    OnListMenuItemClick.Invoke(_selectedItem);
                    listView.SelectedItem = null;
                }
            }
        }
        class ReverseConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var s = value as ICollection<Models.MenuItem>;
                return s != null && s.Count > 0;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var s = value as ICollection<MenuItem>;
                return !(s != null && s.Count > 0);
            }
        }
    }
}

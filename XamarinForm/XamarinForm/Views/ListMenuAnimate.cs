using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Delegates;

namespace XamarinForm.Views
{
    public class ListMenuAnimate<T> : StackLayout where T : Models.MenuItem
    {
        /// <summary>
        /// 菜单项被点击时触发
        /// </summary>
        public ListMenuItemClickHandle<Models.MenuItem> OnListMenuItemClick { private get; set; }

        public T MenuItem { get; private set; }

        /// <summary>
        /// 当前菜单
        /// </summary>
        public Models.MenuItem CurrentMenuItem { get; private set; }

        private Label titleBackLabel;
        private Label titleLabel;
        private StackLayout titlLayout;
        private AbsoluteLayout menuLayout;
        private ListView parentMenuView;
        private ListView menuView;

        public ListMenuAnimate(T menuItem)
        {
            IsClippedToBounds = true;
            Orientation = StackOrientation.Vertical;
            VerticalOptions = LayoutOptions.Fill;
            HorizontalOptions = LayoutOptions.Fill;
            Padding = new Thickness(5, 5, 5, 5);

            MenuItem = menuItem;
            CurrentMenuItem = menuItem;

            titlLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.BlueViolet,
                Padding = new Thickness(0, 5, 0, 5),
            };

            titleBackLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.Blue,
            };
            titleBackLabel.FontSize = Device.GetNamedSize(NamedSize.Default, titleBackLabel);

            titleLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            titleLabel.FontSize = Device.GetNamedSize(NamedSize.Default, titleLabel);
            
            titleBackLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(clickBack)
            });
            titleBackLabel.FontAttributes = FontAttributes.Bold;
            titleLabel.FontAttributes = FontAttributes.Bold;
            
            //menuView = new ListView()
            //{
            //    VerticalOptions=LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.Fill,
            //    BackgroundColor = Color.White,
            //    ItemTemplate = new DataTemplate(() => { return new ListMenuItem<T>(); })
            //};


            menuLayout = new AbsoluteLayout
            {
                VerticalOptions =LayoutOptions.FillAndExpand,
                HorizontalOptions=LayoutOptions.FillAndExpand,
                BackgroundColor=Color.Red,
            };

            titlLayout.Children.Add(titleBackLabel);
            titlLayout.Children.Add(titleLabel);

            Children.Add(titlLayout);
            Children.Add(menuLayout);
            
            NextMenu();
            bindingData();
        }
        
        private void bindingData()
        {
            if (CurrentMenuItem.ParentMenuItem != null)
            {
                titleBackLabel.IsEnabled = true;
                titleBackLabel.Text = "<返回";
            }
            else
            {
                titleBackLabel.IsEnabled = false;
                titleBackLabel.Text = "";
            }
            titleBackLabel.IsEnabled = true;
            titleBackLabel.Text = "<返回";
            titleLabel.Text = CurrentMenuItem.Title;
            menuView.ItemsSource = CurrentMenuItem.ChildrenMenu;
        }
        private void clickBack()
        {
            titleBackLabel.Opacity = 0.65;
            titleBackLabel.TextColor = Color.LightGray;

            Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                if (CurrentMenuItem.ParentMenuItem != null)
                {
                    CurrentMenuItem = CurrentMenuItem.ParentMenuItem;
                }
                //listView.SelectedItem = null;

                titleBackLabel.Opacity = 1;
                titleBackLabel.TextColor = Color.Blue;

                return false;
            });

            NextMenu();
            bindingData();
        }
        
        private void NextMenu()
        {
            double width = menuLayout.Width;
            if(menuView!=null) parentMenuView = menuView;
            if (parentMenuView == null) width = 0;
            menuView = new ListView()
            {
                BackgroundColor=Color.White,
                TranslationX = width,
                WidthRequest= width,
                HeightRequest=menuLayout.Height,
                ItemTemplate = new DataTemplate(() => { return new ListMenuItem<T>(); })
            };

            menuLayout.Children.Add(menuView);

            Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                if (menuView.TranslationX <= 60)
                {
                    menuView.TranslationX = 0;
                    return false;
                }
                
                menuView.TranslationX -= 60;
                return true;
            });
        }

        class MenuListView : ListView
        {
            
        }
    }
}

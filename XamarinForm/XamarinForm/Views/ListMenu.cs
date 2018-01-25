using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Delegates;

namespace XamarinForm.Views
{
    public class ListMenu<T> : StackLayout where T : Models.MenuItem
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
        private ListView listView;
        private StackLayout titlLayout;

        public ListMenu(T menuItem)
        {
            MenuItem = menuItem;
            CurrentMenuItem = menuItem;

            titlLayout = new StackLayout
            {
                Orientation=StackOrientation.Horizontal,
                VerticalOptions=LayoutOptions.Start,
                HorizontalOptions=LayoutOptions.Fill,
                BackgroundColor=Color.BlueViolet,
                Padding=new Thickness(0,5,0,5),
            };

            titleBackLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor=Color.Blue,
            };
            titleBackLabel.FontSize = Device.GetNamedSize(NamedSize.Default, titleBackLabel);

            titleLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            titleLabel.FontSize = Device.GetNamedSize(NamedSize.Default, titleLabel);

            listView = new ListView()
            {
                ItemTemplate = new DataTemplate(() => { return new ListMenuItem<T>(); })
            };

            listView.ItemSelected += ListView_ItemSelected;

            IsClippedToBounds = true;
            Orientation = StackOrientation.Vertical;
            VerticalOptions = LayoutOptions.Fill;
            HorizontalOptions = LayoutOptions.Fill;
            Padding = new Thickness(5, 5, 5, 5);
            titleBackLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(clickBack)
            });
            //titleBackLabel.FontAttributes = FontAttributes.Bold;
            titleLabel.FontAttributes = FontAttributes.Bold;

            titlLayout.Children.Add(titleBackLabel);
            titlLayout.Children.Add(titleLabel);

            Children.Add(titlLayout);
            Children.Add(listView);
            bindingData();
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
                listView.SelectedItem = null;

                bindingData();
                titleBackLabel.Opacity = 1;
                titleBackLabel.TextColor = Color.Blue;

                return false;
            });
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.MenuItem _selectedItem = e.SelectedItem as Models.MenuItem;
            if (_selectedItem == null)
            {
                return;
                //throw new Exception("未获取到选择的菜单项！");
            }

            if (OnListMenuItemClick == null)
            {
                CurrentMenuItem = _selectedItem;
                bindingData();
            }
            else
            {
                if (OnListMenuItemClick.Invoke(_selectedItem))
                {
                    CurrentMenuItem = _selectedItem;
                    bindingData();
                }
                
            }
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
            titleLabel.Text = CurrentMenuItem.Title;
            listView.ItemsSource = CurrentMenuItem.ChildrenMenu;
        }
        public void Renderer()
        {
            VisualElement visualElement = Parent as VisualElement;
            HeightRequest = visualElement.Height;
            WidthRequest = visualElement.WidthRequest;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XamarinForm.Views
{
    public class GridMenuItem : StackLayout
    {
        public Models.MenuItem Item { get; set; }
        private Label label;
        private Image image;

        public Label Title { get; private set; }

        public Image Image { get; private set; }

        public GridMenuItem(Models.MenuItem Item)
        {
            this.Item = Item;

            image = new Image
            {
                Source = Item.Icon,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            //添加点击手势
#pragma warning disable CS0618 // 类型或成员已过时
            GestureRecognizers.Add(item: new TapGestureRecognizer((sender, args) =>
            {
                GridMenuItem layout = (GridMenuItem)sender;
                layout.Opacity = 0.75;

                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    layout.Opacity = 1;
                    return false;
                });
                //  其他操作
                // layout.Item
                Console.WriteLine("点击{0}", layout.Item.MenuItemId);
            }));
#pragma warning restore CS0618 // 类型或成员已过时

            label = new Label
            {
                Text = Item.Title,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            label.FontSize = Device.GetNamedSize(NamedSize.Micro, label);

            IsClippedToBounds = true;
            Orientation = StackOrientation.Vertical;
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            Padding = new Thickness(1, 2, 1, 1);
            Children.Add(image);
            Children.Add(label);

        }
    }
}
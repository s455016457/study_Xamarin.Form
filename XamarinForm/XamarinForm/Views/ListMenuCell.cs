using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    public class ListMenuCell : ViewCell
    {
        Image image1;
        Label label;
        Image image2;
        StackLayout stackLayout;

        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(ImageSource), typeof(ListMenuCell));
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(String), typeof(ListMenuCell), "");
        public static readonly BindableProperty HasChildProperty = BindableProperty.Create("HasChild", typeof(Boolean), typeof(ListMenuCell), false);

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public Boolean HasChild
        {
            get { return (Boolean)GetValue(HasChildProperty); }
            set { SetValue(HasChildProperty, value); }
        }

        public ListMenuCell()
        {
            stackLayout = new StackLayout();
            image1 = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            label = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            label.FontSize = Device.GetNamedSize(NamedSize.Micro, label);

            image2 = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            stackLayout.IsClippedToBounds = true;
            stackLayout.Orientation = StackOrientation.Horizontal;
            stackLayout.VerticalOptions = LayoutOptions.Fill;
            stackLayout.HorizontalOptions = LayoutOptions.Fill;

            stackLayout.Children.Add(image1);
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(image2);

            View = stackLayout;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            image1.Source = Icon;
            label.Text = Title;
            if (HasChild)
            {
                image2.Source = ImageSource.FromFile("folder.png");
            }
            else
            {
                image2.Source = ImageSource.FromFile("page_white_text.png");
            }
        }
    }
}

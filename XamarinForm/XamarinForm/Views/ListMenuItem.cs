using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    public class ListMenuItem <T>: ViewCell where T : Models.MenuItem
    {
        private Image image1;
        private Label label;
        private Image image2;
        private StackLayout stackLayout;

        public ListMenuItem()
        {
            stackLayout = new StackLayout();
            image1 = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                //HorizontalOptions = LayoutOptions.Start,
            };
            image1.SetBinding(Image.SourceProperty, "Icon");

            label = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            label.FontSize = Device.GetNamedSize(NamedSize.Micro, label);
            label.SetBinding(Label.TextProperty, "Title");
            //label.BackgroundColor = Color.Blue;

            image2 = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                //HorizontalOptions = LayoutOptions.End,
            };
            image2.SetBinding(Image.SourceProperty, new Binding("ChildrenMenu", converter: new ReverseConverter()));

            stackLayout.IsClippedToBounds = true;
            stackLayout.Orientation = StackOrientation.Horizontal;
            stackLayout.VerticalOptions = LayoutOptions.Fill;
            stackLayout.HorizontalOptions = LayoutOptions.Fill;
            //stackLayout.BackgroundColor = Color.Green;

            stackLayout.Children.Add(image1);
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(image2);

            View = stackLayout;
        }

        class ReverseConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var s = value as ICollection<Models.MenuItem>;
                if (s == null)
                    return ImageSource.FromFile("page_white_text.png");

                if (s.Count > 0)
                    return ImageSource.FromFile("folder.png");

                return ImageSource.FromFile("page_white_text.png");
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var s = value as ICollection<MenuItem>;
                if (s == null)
                    return ImageSource.FromFile("page_white_text.png");

                if (s.Count > 0)
                    return ImageSource.FromFile("folder.png");

                return ImageSource.FromFile("page_white_text.png");
            }
        }
    }
}

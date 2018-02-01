using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm.Pages.Control
{
    public class TestLabelPage : ControlBasePage
    {
        public TestLabelPage() : base()
        {
            Title = "Label标签";
        }

        protected override View GetView()
        {
            StackLayout stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical
            };
            stackLayout.Children.Add(new Label
            {
                Text = "This is Default Label"
            });
            stackLayout.Children.Add(new Label
            {
                Text = "This Label TextColor is Red and the FontAttributes is Bold and the FontSize is Large",
                TextColor = Color.Red,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });
            stackLayout.Children.Add(new Label
            {
                Text = "This Label TextColor is Blue and the FontAttributes is Italic and the FontSize is Medium",
                TextColor = Color.Blue,
                FontAttributes = FontAttributes.Italic,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });
            stackLayout.Children.Add(new Label
            {
                Text = "This Label FontSize is Micro",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
            });
            stackLayout.Children.Add(new Label
            {
                Text = "This Label FontSize is Small",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            });

            return stackLayout;
        }
    }
}
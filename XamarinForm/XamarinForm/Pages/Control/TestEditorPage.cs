using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Control
{
    public class TestEditorPage : ControlBasePage
    {
        public TestEditorPage() : base()
        {
            Title = "多行输入框";
        }
        protected override View GetView()
        {
            StackLayout stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical
            };

            stackLayout.Children.Add(new Editor
            {
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.DarkGreen,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Text= "This is Editor,the FongAttributes is bold,TextColor is DarkGreen,FontSize is Micro"
            });

            stackLayout.Children.Add(new Editor
            {
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.DarkGreen,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Text = "This is Editor,the FongAttributes is Italic,TextColor is DarkGreen,FontSize is Micro,HeightRequest is 40",
                HeightRequest=80
            });

            return stackLayout;
        }
    }
}

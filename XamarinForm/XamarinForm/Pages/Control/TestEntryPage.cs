using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Control
{
    public class TestEntryPage : ControlBasePage
    {
        public TestEntryPage() : base()
        {
            Title = "输入框";
        }
        protected override View GetView()
        {
            StackLayout stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical
            };
            stackLayout.Children.Add(new Entry
            {
                Placeholder = "default Entry"
            });
            stackLayout.Children.Add(new Entry
            {
                PlaceholderColor=Color.Silver,
                Keyboard= Keyboard.Chat,
                Placeholder = "PlaceholderColor is Silver , Keyboard is Chat"
            });
            stackLayout.Children.Add(new Entry
            {
                IsPassword = true,
                PlaceholderColor = Color.Silver,
                Keyboard = Keyboard.Default,
                Placeholder = "password Entry, Keyboard is Default"
            });
            stackLayout.Children.Add(new Entry
            {
                TextColor=Color.SteelBlue,
                PlaceholderColor = Color.Silver,
                Keyboard=Keyboard.Email,
                Placeholder = "TextColor is SteelBlue,Keyboard is Email"
            });
            stackLayout.Children.Add(new Entry
            {
                FontAttributes=FontAttributes.Bold,
                PlaceholderColor = Color.Silver,
                Keyboard=Keyboard.Numeric,
                Placeholder = "FontAttributes is Bold,Keyboard is Numeric"
            });
            stackLayout.Children.Add(new Entry
            {
                FontAttributes =FontAttributes.Italic,
                PlaceholderColor = Color.Silver,
                Keyboard=Keyboard.Plain,
                Placeholder = "FontAttributes is Italic,Keyboard is Plain",
            });
            stackLayout.Children.Add(new Entry
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                PlaceholderColor = Color.Silver,
                Keyboard=Keyboard.Telephone,
                Placeholder = "FontSize is Large,Keyboard is Telephone",
            });
            stackLayout.Children.Add(new Entry
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                PlaceholderColor = Color.Silver,
                Keyboard=Keyboard.Text,
                Placeholder = "FontSize is Medium,Keyboard is Text",
            });
            stackLayout.Children.Add(new Entry
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                PlaceholderColor = Color.Silver,
                Keyboard= Keyboard.Url,
                Placeholder = "FontSize is Micro,Keyboard is Url",
            });
            stackLayout.Children.Add(new Entry
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                PlaceholderColor = Color.Silver,
                Placeholder = "FontSize is Small",
            });
            stackLayout.Children.Add(new Entry
            {
                HorizontalTextAlignment = TextAlignment.Start,
                PlaceholderColor = Color.Silver,
                Placeholder = "HorizontalTextAlignment is Start",
            });
            stackLayout.Children.Add(new Entry
            {
                HorizontalTextAlignment = TextAlignment.Center,
                PlaceholderColor = Color.Silver,
                Placeholder = "HorizontalTextAlignment is Center",
            });
            stackLayout.Children.Add(new Entry
            {
                HorizontalTextAlignment = TextAlignment.End,
                PlaceholderColor = Color.Silver,
                Placeholder = "HorizontalTextAlignment is End",
            });

            return stackLayout;
        }
    }
}

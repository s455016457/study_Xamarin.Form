using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Views;

namespace XamarinForm.Pages.Effect
{
    public class TestLeftSlideContentViewPage : ContentPage
    {
        public TestLeftSlideContentViewPage()
        {
            Title = "测试左滑效果";

            StackLayout stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Orientation=StackOrientation.Horizontal,
                Children = { new Label { Text = "左滑效果1", HorizontalOptions = LayoutOptions.Fill } , new Label { Text = "左滑效果2", HorizontalOptions = LayoutOptions.Fill } },
            };

            AbsoluteLayout absoluteLayout = new AbsoluteLayout()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Blue,
                Children = {
                    new LeftSlideContentView{
                        Padding=new Thickness(30),
                        BackgroundColor=Color.White,
                        Content=new BoxView{ BackgroundColor=Color.Red,WidthRequest=50,HeightRequest=25},
                        HorizontalOptions=LayoutOptions.End,
                    },
                    new LeftSlideContentView {
                        Padding=new Thickness(30),
                        BackgroundColor=Color.White,
                        Content=stackLayout,
                        HorizontalOptions =LayoutOptions.Fill,
                    },
                },
            };
            stackLayout.Children.Add(new Label { Text = "左滑效果3", HorizontalOptions = LayoutOptions.Fill });

            Content = absoluteLayout;
        }
    }
}

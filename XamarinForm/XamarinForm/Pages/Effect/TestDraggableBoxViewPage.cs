using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Views;

namespace XamarinForm.Pages.Effect
{
    public class TestDraggableBoxViewPage : ContentPage
    {
        Random random = new Random();
        AbsoluteLayout absoluteLayout;
        public TestDraggableBoxViewPage()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            Button addButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "添加矩形",
            };
            addButton.Clicked += AddButton_Clicked;

            Button clearButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "清空",
            };

            clearButton.Clicked += ClearButton_Clicked;

            StackLayout topLayout = new StackLayout
            {
                HorizontalOptions=LayoutOptions.CenterAndExpand,
                Orientation =StackOrientation.Horizontal,
                Children = {
                    addButton,clearButton
                }
            };

            absoluteLayout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor=Color.AliceBlue,
            };

            Label bottomLable = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                Text="这里是底部内容"
            };

            Label leftLable = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                WidthRequest = 20,
                Text = "左边内容"
            };

            Label rightLable = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                WidthRequest = 20,
                Text = "右边内容"
            };

            grid.Children.Add(topLayout, 0, 0);
            grid.Children.Add(leftLable, 0,1);
            grid.Children.Add(absoluteLayout,1,1);
            grid.Children.Add(rightLable, 2,1);
            grid.Children.Add(bottomLable, 0, 2);
            Grid.SetColumnSpan(bottomLable, 3);
            Grid.SetColumnSpan(topLayout, 3);

            Content = grid;
            AddBoxViewToLayout();
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            absoluteLayout.Children.Clear();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            AddBoxViewToLayout();
        }

        void AddBoxViewToLayout()
        {
            absoluteLayout.Children.Add(new DraggableBoxView
            {
                WidthRequest = 100,
                HeightRequest = 100,
                Color = new Color(random.NextDouble(), random.NextDouble(), random.NextDouble())
            });
        }
    }
}

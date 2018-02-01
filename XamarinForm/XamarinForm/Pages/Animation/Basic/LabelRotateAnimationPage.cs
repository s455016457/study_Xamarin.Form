using System;
using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
    public class LabelRotateAnimationPage : ContentPage
    {
        Label label;
        AbsoluteLayout absoluteLayout;
        Button startButton, stopButton;
        public LabelRotateAnimationPage()
        {
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnDefinitions = {
                    new ColumnDefinition()
                },
                RowDefinitions = {
                    new RowDefinition{
                        Height =GridLength.Star
                    },
                    new RowDefinition{
                        Height =GridLength.Auto
                    },
                    new RowDefinition{
                        Height =GridLength.Auto
                    }
                },
            };
            Title = "文字旋转效果";
            label = new Label
            {
                Text = "文字旋转效果",
                BackgroundColor = Color.Gray,
                VerticalOptions=LayoutOptions.CenterAndExpand,
                HorizontalOptions=LayoutOptions.CenterAndExpand,
            };
            label.FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label));

            label.SizeChanged += OnViewSizeChanged;

            absoluteLayout = new AbsoluteLayout
            {
                HorizontalOptions=LayoutOptions.Fill,
                VerticalOptions=LayoutOptions.Fill,
                Children ={
                    label,
                },
            };

            startButton = new Button
            {
                Text = "开始",
                VerticalOptions = LayoutOptions.End,
            };
            startButton.Clicked += StartButton_Clicked;

            stopButton = new Button
            {
                Text = "停止",
                VerticalOptions = LayoutOptions.End,
            };
            stopButton.Clicked += StopButton_Clicked;

            grid.Children.Add(absoluteLayout, 0, 0);
            grid.Children.Add(startButton, 0, 1);
            grid.Children.Add(stopButton, 0, 2);

            SetButtonStact(false, true);
            Content = grid;
        }

        private void StopButton_Clicked(object sender, System.EventArgs e)
        {
            ViewExtensions.CancelAnimations(label);//清除元素的LayoutTo, RotateTo, ScaleTo，和FadeTo动画效果
            SetButtonStact(false, true);
        }
        void OnViewSizeChanged(object sender, EventArgs e)
        {
            var center = new Point(absoluteLayout.Width / 2, absoluteLayout.Height / 2);
            var view = sender as View;
            //设置控件的位置
            AbsoluteLayout.SetLayoutBounds(view, new Rectangle(center.X - view.Width / 2, center.Y - view.Height / 2, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
        }

        private async void StartButton_Clicked(object sender, System.EventArgs e)
        {
            SetButtonStact(true, false);

            //label.AnchorY = (Math.Min(absoluteLayout.Width, absoluteLayout.Height) / 2) / label.Height;
            label.AnchorY = 4.2;
            await label.RotateTo(360, 2000);
            label.Rotation = 0;
            SetButtonStact(false, true);
        }

        void SetButtonStact(bool stopButtonb, bool startButtonb)
        {
            stopButton.IsEnabled = stopButtonb;
            startButton.IsEnabled = startButtonb;
        }
    }
}

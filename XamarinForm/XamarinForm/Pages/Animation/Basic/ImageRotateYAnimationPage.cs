using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
	public class ImageRotateYAnimationPage : ContentPage
    {
        Image image;
        Button startButton, stopButton;
        public ImageRotateYAnimationPage()
		{
            Title = "沿Y轴旋转";
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
            image = new Image
            {
                Source = ImageSource.FromFile("meinv.jpg"),
                BackgroundColor = Color.Gray,
                Aspect = Aspect.AspectFit,
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

            grid.Children.Add(image, 0, 0);
            grid.Children.Add(startButton, 0, 1);
            grid.Children.Add(stopButton, 0, 2);

            SetButtonStact(false, true);
            Content = grid;
        }

        private void StopButton_Clicked(object sender, System.EventArgs e)
        {
            ViewExtensions.CancelAnimations(image);//清除元素的LayoutTo, RotateTo, ScaleTo，和FadeTo动画效果
            SetButtonStact(false, true);
        }

        private async void StartButton_Clicked(object sender, System.EventArgs e)
        {
            SetButtonStact(true, false);

            await image.RotateYTo(360, 4000);
            image.RotationY = 0;
            SetButtonStact(false, true);
        }

        void SetButtonStact(bool stopButtonb, bool startButtonb)
        {
            stopButton.IsEnabled = stopButtonb;
            startButton.IsEnabled = startButtonb;
        }
    }
}
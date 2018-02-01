using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Custom
{
    public class ImageScaleAnimationPage2 : ContentPage
    {
        Image image;
        Picker picker;
        Button startButton, stopButton;
        Dictionary<string, Easing> nameToEassing = new Dictionary<string, Easing>
        {
            { "Linear【线性变换】", Easing.Linear },
            { "BounceIn【跳向最终值并弹回】", Easing.BounceIn },
            { "BounceOut【跳跃到最终的值，跳跃3次，然后稳定下来】", Easing.BounceOut },
            { "CubicIn【加速】", Easing.CubicIn },
            { "CubicOut【减速】", Easing.CubicOut },
            { "CubicInOut【加速和加速】", Easing.CubicInOut },
            { "SinIn【平稳加速】", Easing.SinIn },
            { "SinOut【平稳减速】", Easing.SinOut }, 
            { "SinInOut【加速并减速】", Easing.SinInOut },
            { "SpringIn【移开，然后跳到最后的值】", Easing.SpringIn },
            { "SpringOut【越过，然后返回】", Easing.SpringOut },
        };


        public ImageScaleAnimationPage2()
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
                    },
                    new RowDefinition{
                        Height =GridLength.Auto
                    }
                },
            };
            Title = "图片循环放大效果";
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

            picker = new Picker
            {
                Title = "动画效果",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string easingName in nameToEassing.Keys)
            {
                picker.Items.Add(easingName);
            }

            grid.Children.Add(image, 0, 0);
            grid.Children.Add(picker, 0, 1);
            grid.Children.Add(startButton, 0, 2);
            grid.Children.Add(stopButton, 0, 3);

            SetButtonStact(false, true);
            Content = grid;
        }

        private void StopButton_Clicked(object sender, System.EventArgs e)
        {
            this.AbortAnimation("SimpleAnimation");//停止动画
            SetButtonStact(false, true);
        }

        private void StartButton_Clicked(object sender, System.EventArgs e)
        {
            SetButtonStact(true, false);
            Easing easing = Easing.Linear;
            if (picker.SelectedIndex >= 0)
            {
                string easingName = picker.Items[picker.SelectedIndex];
                easing = nameToEassing[easingName];
            }

            Xamarin.Forms.Animation animation = new Xamarin.Forms.Animation(v => image.Scale = v, 1, 2, easing);
            animation.Commit(this, "SimpleAnimation", 16, 2000, Easing.Linear, (v, c) => { image.Scale = 1; }, () => true);
        }

        void SetButtonStact(bool stopButtonb, bool startButtonb)
        {
            stopButton.IsEnabled = stopButtonb;
            picker.IsEnabled = startButtonb;
            startButton.IsEnabled = startButtonb;
        }
    }
}

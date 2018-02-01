using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
    public class ImageRelativeRotateAnimationPage : ContentPage
    {
            Image image;
            Button startButton, stopButton;
        public ImageRelativeRotateAnimationPage()
        {
            Grid grid = new Grid
            {
                HorizontalOptions=LayoutOptions.Fill,
                VerticalOptions=LayoutOptions.Fill,
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
            Title = "图片旋转效果";
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

            await image.RelRotateTo(360, 2000);
            SetButtonStact(false, true);
        }

        void SetButtonStact(bool stopButtonb,bool startButtonb)
        {
            stopButton.IsEnabled = stopButtonb;
            startButton.IsEnabled = startButtonb;
        }
    }
}

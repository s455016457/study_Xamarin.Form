using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
    public class ImageRelativeScaleAnimationPage : ContentPage
    {
            Image image;
            Button startButton, stopButton;
        public ImageRelativeScaleAnimationPage()
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
            Title = "图片缩放效果";
            image = new Image
            {
                Source = ImageSource.FromFile("meinv.jpg"),
                //BackgroundColor = Color.Gray,
                Aspect = Aspect.AspectFit,
            };

            startButton = new Button
            {
                Text = "放大",
                VerticalOptions = LayoutOptions.End,
            };
            startButton.Clicked += StartButton_Clicked;

            stopButton = new Button
            {
                Text = "缩小",
                VerticalOptions = LayoutOptions.End,
            };
            stopButton.Clicked += StopButton_Clicked;

            grid.Children.Add(image, 0, 0);
            grid.Children.Add(startButton, 0, 1);
            grid.Children.Add(stopButton, 0, 2);

            //SetButtonStact(true, true);
            Content = grid;
        }

        private async void StopButton_Clicked(object sender, System.EventArgs e)
        {
            SetButtonStact(false, false);
            await image.RelScaleTo(-2, 2000);
            SetButtonStact(true, true);
        }

        private async void StartButton_Clicked(object sender, System.EventArgs e)
        {
            SetButtonStact(false, false);

            await image.RelScaleTo(2, 2000);
            SetButtonStact(true, true);
        }

        void SetButtonStact(bool stopButtonb,bool startButtonb)
        {
            stopButton.IsEnabled = stopButtonb;
            startButton.IsEnabled = startButtonb;
        }
    }
}

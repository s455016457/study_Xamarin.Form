using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Custom
{
    public class ImageChildAnimationPage : ContentPage
    {
        Image image;
        Button startButton, stopButton;


        public ImageChildAnimationPage()
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

            grid.Children.Add(image, 0, 0);
            grid.Children.Add(startButton, 0, 1);
            grid.Children.Add(stopButton, 0, 2);

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
            Xamarin.Forms.Animation animation = new Xamarin.Forms.Animation();
            Xamarin.Forms.Animation animationUp = new Xamarin.Forms.Animation(v => image.Scale = v, 1, 2,Easing.SpringIn);
            Xamarin.Forms.Animation animationDown = new Xamarin.Forms.Animation(v => image.Scale = v, 2, 1,Easing.SpringOut);
            Xamarin.Forms.Animation animationRotation = new Xamarin.Forms.Animation(v => image.Rotation = v, 0, 360,Easing.Linear);

            animation.Add(0, 0.5, animationUp);
            animation.Add(0, 1, animationRotation);
            animation.Add(0.5, 1, animationDown);

            animation.Commit(this, "SimpleAnimation", 16, 4000, Easing.Linear, (v, c) => { image.Scale = 1; image.Rotation = 0; }, () => true);
        }

        void SetButtonStact(bool stopButtonb, bool startButtonb)
        {
            stopButton.IsEnabled = stopButtonb;
            startButton.IsEnabled = startButtonb;
        }
    }
}

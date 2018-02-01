using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Custom
{
    public class ImageChildAnimationPage2 : ContentPage
    {
        Image image;
        Button startButton, stopButton;


        public ImageChildAnimationPage2()
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
                //BackgroundColor = Color.Gray,
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

            Xamarin.Forms.Animation animationScale = new Xamarin.Forms.Animation();
            Xamarin.Forms.Animation animationScaleUp = new Xamarin.Forms.Animation(v => image.Scale = v, 0.5, 1,Easing.SpringIn);
            Xamarin.Forms.Animation animationScaleDown = new Xamarin.Forms.Animation(v => image.Scale = v, 1, 0.5,Easing.SpringOut);
            Xamarin.Forms.Animation animationRotation = new Xamarin.Forms.Animation(v => image.Rotation = v, 0, 15*360,Easing.Linear);
            Xamarin.Forms.Animation animationRotationX = new Xamarin.Forms.Animation(v => image.RotationX = v, 0, 12*360,Easing.Linear);
            Xamarin.Forms.Animation animationRotationY = new Xamarin.Forms.Animation(v => image.RotationY = v, 0, 9*360,Easing.Linear);

            animation.Add(0, 0.5, animationScaleDown);
            animation.Add(0.5, 1, animationScaleUp);

            animation.Add(0, 1, animationRotation);
            animation.Add(0, 1, animationRotationX);
            animation.Add(0, 1, animationRotationY);
            animation.Add(0, 1, animationScale);

            animation.Commit(this, "SimpleAnimation", 16, 40000, Easing.Linear, (v, c) => { image.Scale = 1; image.Rotation = 0; image.RotationX = 0; image.RotationY = 0; }, () => true);
        }

        void SetButtonStact(bool stopButtonb, bool startButtonb)
        {
            stopButton.IsEnabled = stopButtonb;
            startButton.IsEnabled = startButtonb;
        }
    }
}

using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
    public class ImageScaleAnimationPage : ContentPage
    {
        Image image;
        public ImageScaleAnimationPage()
        {
            Grid grid = new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition()
                },
                RowDefinitions = {
                    new RowDefinition{
                        Height =GridLength.Star
                    },
                    new RowDefinition{
                        Height =GridLength.Auto
                    }
                },
            };
            Title = "图片缩放效果";
            image = new Image
            {
                Source= ImageSource.FromFile("meinv.jpg"),
                BackgroundColor=Color.Gray,
                Aspect=Aspect.AspectFit,
            };

            Button button = new Button
            {
                Text = "开始",
                VerticalOptions = LayoutOptions.End,
            };

            button.Clicked += Button_Clicked;

            grid.Children.Add(image,0,0);
            grid.Children.Add(button,0,1);

            Content = grid;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            Button button = sender as Button;
            button.IsEnabled = false;
            bool isCancelled = await image.ScaleTo(0.5, 2000);
            if (!isCancelled) {
                await image.ScaleTo(1, 2000);
            }
            button.IsEnabled = true;
        }
    }
}

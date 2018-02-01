using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
    public class ImageTranslateAnimationPage : ContentPage
    {
        Image image;
        public ImageTranslateAnimationPage()
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
            Title = "图片移动效果";
            image = new Image
            {
                Source= ImageSource.FromFile("meinv.jpg"),
                BackgroundColor=Color.Gray,
                Aspect=Aspect.AspectFit,
                //HorizontalOptions=LayoutOptions.CenterAndExpand,
                //VerticalOptions =LayoutOptions.CenterAndExpand,
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
            bool isCancelled = await image.TranslateTo(-100, 0, 2000);
            if (!isCancelled)
            {
                isCancelled = await image.TranslateTo(-100, -100, 2000);
            }
            if (!isCancelled)
            {
                isCancelled = await image.TranslateTo(100, 100, 2000);
            }
            if (!isCancelled)
            {
                isCancelled = await image.TranslateTo(0, 100, 2000);
            }
            if (!isCancelled)
            {
                isCancelled = await image.TranslateTo(0, 0, 2000);
            }
            button.IsEnabled = true;
        }
    }
}

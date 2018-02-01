using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
    public class ImageFadeAnimationPage : ContentPage
    {
        Image image;
        public ImageFadeAnimationPage()
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
            Title = "淡入效果";
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
            image.Opacity = 0;
            await image.FadeTo(1, 4000);
            button.IsEnabled = true;
        }
    }
}

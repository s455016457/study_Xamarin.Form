using Xamarin.Forms;

namespace XamarinForm.Pages.NavigationPages
{
    public class Page2 : ContentPage
    {
        public Page2()
        {
            Title = "页面2";
            
            var button = new Button
            {
                Text = "上一页",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
            };
            button.Clicked += button_Clicked;

            var nextButton = new Button
            {
                Text = "下一页",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
            };
            nextButton.Clicked += nextButton_Clicked;


            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    new Label{
                        Text="这里是页面2"
                    },
                    button,
                    nextButton
                }
            };
        }

        private async void button_Clicked(object sender, System.EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                await navigation.PopAsync(true);
            }
        }
        private async void nextButton_Clicked(object sender, System.EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                Page3 animationPage = new Page3();
                animationPage.Opacity = 0;
                await Navigation.PushAsync(animationPage, true);
                await animationPage.FadeTo(1, 500);
            }
        }
    }
}

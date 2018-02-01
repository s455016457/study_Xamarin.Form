using Xamarin.Forms;

namespace XamarinForm.Pages.NavigationPages
{
    public class Page1:ContentPage
    {
        public Page1()
        {
            Title = "页面1";

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
                        Text="这里是页面1"
                    },
                    nextButton
                }
            };
        }

        private async void nextButton_Clicked(object sender, System.EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                Page2 animationPage = new Page2();
                animationPage.Opacity = 0;
                await Navigation.PushAsync(animationPage, true);
                await animationPage.FadeTo(1, 500);
            }
        }
    }
}

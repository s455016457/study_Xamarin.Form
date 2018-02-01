using Xamarin.Forms;

namespace XamarinForm.Pages.NavigationPages
{
    public class Page3 : ContentPage
    {
        public Page3()
        {
            Title = "页面3";
            var nextButton = new Button
            {
                Text = "上一页",
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
                        Text="这里是页面3"
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
                await navigation.PopAsync(true);
            }
        }
    }
}

using Xamarin.Forms;

namespace XamarinForm.Pages.Animation.Basic
{
    public class LabelFadeAnimationPage:ContentPage
    {
        public LabelFadeAnimationPage()
        {
            Title = "文字淡入效果";
            Label label = new Label
            {
                Text="文字淡入效果",
                BackgroundColor=Color.Gray,
                HorizontalOptions=LayoutOptions.Center,
                VerticalOptions=LayoutOptions.Center,
            };
            Content = new ContentView
            {
                Content= label
            };

            label.Opacity = 0;
            label.FadeTo(1, 4000);
        }
    }
}

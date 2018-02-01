using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.Extensions;

namespace XamarinForm.Pages.Animation.Custom
{
    public class ColorAnimationPage : ContentPage
    {
        BoxView boxView;
        Label label;
        Button cancelButton;
        public ColorAnimationPage()
        {

            boxView = new BoxView
            {
                HeightRequest = 100,
                Color = Color.Blue,
                HorizontalOptions = LayoutOptions.Fill,
            };

            label = new Label
            {
                Text = "文字显示效果",
                TextColor = Color.Default,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            label.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));

            Button boxViewButton = new Button
            {
                Text = "BoxView背景颜色动画",
            };
            boxViewButton.Clicked += BoxViewButton_ClickedAsync;

            Button labelButton = new Button
            {
                Text = "Label背景颜色动画",
            };
            labelButton.Clicked += LabelButton_ClickedAsync;

            Button pageButton = new Button
            {
                Text = "页面背景颜色动画",
            };
            pageButton.Clicked += PageButton_ClickedAsync;

            cancelButton = new Button
            {
                Text = "取消",
            };
            cancelButton.Clicked += CancelButton_ClickedAsync;

            Content = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                Children = {
                    boxView,
                    boxViewButton,
                    label,
                    labelButton,
                    pageButton,
                    cancelButton,
                }
            };
        }

        void SetIsEnabledCancelButtonState(bool cancelButtonState)
        {
            cancelButton.IsEnabled = cancelButtonState;
        }

        void SetIsEnabledButton(object obj, bool isEnabled)
        {
            Button button = obj as Button;
            if (button != null)
                button.IsEnabled = isEnabled;
        }

        private async void BoxViewButton_ClickedAsync(object sender, System.EventArgs e)
        {
            SetIsEnabledCancelButtonState(true);
            SetIsEnabledButton(sender, false);
            await boxView.ColorTo(Color.Blue, Color.Red, c => boxView.Color = c, 4000);
            SetIsEnabledButton(sender, true);
        }

        private void CancelButton_ClickedAsync(object sender, System.EventArgs e)
        {
            this.CancelAnimation();
        }

        private async void PageButton_ClickedAsync(object sender, System.EventArgs e)
        {
            SetIsEnabledCancelButtonState(true);
            SetIsEnabledButton(sender, false);
            await this.ColorTo(Color.FromRgb(0, 0, 0), Color.FromRgb(255, 255, 255), c => BackgroundColor = c, 5000);
            BackgroundColor = Color.Default;
            SetIsEnabledButton(sender, true);
        }
        private async void LabelButton_ClickedAsync(object sender, System.EventArgs e)
        {
            SetIsEnabledCancelButtonState(true);
            SetIsEnabledButton(sender, false);

            await Task.WhenAll(
                label.ColorTo(Color.Red, Color.Blue, c => label.TextColor = c, 5000),
                label.ColorTo(Color.Blue, Color.Red, c => label.BackgroundColor = c, 5000)
            );

            label.BackgroundColor = Color.Default;
            label.TextColor = Color.Default;
            SetIsEnabledButton(sender, true);
        }
    }
}

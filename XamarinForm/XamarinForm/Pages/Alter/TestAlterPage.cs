using Xamarin.Forms;

namespace XamarinForm.Pages.Alter
{
    public class TestAlterPage : ContentPage
    {
        public TestAlterPage()
        {
            Button alterButton = new Button
            {
                Text = "默认Alter"
            };

            Button alter2Button = new Button
            {
                Text = "默认Alter2"
            };

            Button alter3Button = new Button
            {
                Text = "默认Alter3"
            };

            Button alter4Button = new Button
            {
                Text = "默认Alter4"
            };

            alterButton.Clicked += AlterButton_Clicked;
            alter2Button.Clicked += Alter2Button_Clicked;
            alter3Button.Clicked += Alter3Button_Clicked;
            alter4Button.Clicked += Alter4Button_Clicked;

            Content = new StackLayout
            {
                Children = {
                    alterButton,
                    alter2Button,
                    alter3Button,
                    alter4Button,
                }
            };
        }

        private async void Alter4Button_Clicked(object sender, System.EventArgs e)
        {
            string source = await DisplayActionSheet("温馨提示", "确定", "取消", "选项1", "选项2", "选项3");
            await DisplayAlert("结果", source, "确定");
        }

        private async void Alter3Button_Clicked(object sender, System.EventArgs e)
        {
            string source = await DisplayActionSheet("温馨提示", "确定", null, "选项1", "选项2", "选项3", "选项4");
            await DisplayAlert("结果", source, "确定");
        }

        private async void Alter2Button_Clicked(object sender, System.EventArgs e)
        {
            bool source = await DisplayAlert("温馨提示", "这里是提示内容", "确定", "取消");
            await DisplayAlert("结果", source?"确定":"取消", "确定");
        }

        private async void AlterButton_Clicked(object sender, System.EventArgs e)
        {
            await DisplayAlert("温馨提示", "这里是提示内容", "确定");
        }
    }
}

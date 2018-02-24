using Xamarin.Forms;

namespace XamarinForm.Pages.BindableData
{
	public class SimpleColorPickerPage : ContentPage
	{
		public SimpleColorPickerPage ()
		{
            Title = "测试绑定数据";
            BindingContext = new ViewModel.SimpleColorPickerPageViewModel();
            BoxView boxView = new BoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 200,
                Color=Color.Red,
            };
            boxView.SetBinding(BoxView.ColorProperty, "SelectedColor");

            Picker picker = new Picker
            {
                HorizontalOptions =LayoutOptions.Fill,
                Title="请选择颜色",
            };
            picker.SetBinding(Picker.ItemsSourceProperty, "ColorNames");
            picker.SetBinding(Picker.SelectedItemProperty, "SelectedColorName", mode: BindingMode.TwoWay);//get set 属性必须设置为BindingMode.TwoWay才能生效

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                Children = {
                    new Label { Text = "绑定 Picker 数据源", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                    picker,
                    boxView
                }
            };
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm
{
	public class MainPage : BasePage
	{
		public MainPage ():base()
		{
            Title = "示例APP";
		}

        protected override View GetView()
        {
            return new StackLayout
            {
                Children = {
                    new Label { Text = "控件继承结构图" },
                    new Image{ Source="resource.png"},
                },
                Orientation=StackOrientation.Vertical,
                VerticalOptions=LayoutOptions.Fill,
                HorizontalOptions=LayoutOptions.Fill
            };
        }
    }
}
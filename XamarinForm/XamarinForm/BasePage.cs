using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm
{
	public abstract class BasePage : ContentPage
	{
        public BasePage ()
		{
            Button button = new Button() { Text = "菜单" };
            Content = new StackLayout {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical,
                Children = {
                    button,
                    GetView(),
				}
			};
		}

        protected abstract View GetView();
        
    }
}
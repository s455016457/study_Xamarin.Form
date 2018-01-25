using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm
{
	public class ErrorPage : ContentPage
	{
		public ErrorPage (String title,String message)
		{
            Label titleLabel = new Label { Text = title, HorizontalOptions = LayoutOptions.Center };
            titleLabel.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            titleLabel.FontAttributes = FontAttributes.Bold;

            Content = new StackLayout {
				Children = {
                    titleLabel,
					new Label { Text = message }
				},
                HorizontalOptions=LayoutOptions.Fill,
			};
		}
	}
}
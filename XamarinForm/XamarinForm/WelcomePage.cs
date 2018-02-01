using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm
{
	public class WelcomePage : ContentPage
	{
        public WelcomePage()
        {
            Title = "欢迎使用TestApp";
            ToolbarItems.Add(new ToolbarItem("Edit", "", () => { }) { Order = ToolbarItemOrder.Primary });
            Title = "WelComePage";
            Content = new StackLayout
            {
                Children = {
                   new Label { Text = "控件继承结构图" },
                   new ScrollView
                   {
                       //Orientation=ScrollOrientation.Both,
                       Orientation =ScrollOrientation.Vertical,
                       VerticalOptions = LayoutOptions.Fill,
                       HorizontalOptions = LayoutOptions.Fill,
                       Content =new Image{
                           Source ="resource.png",
                           VerticalOptions=LayoutOptions.StartAndExpand,
                           HorizontalOptions=LayoutOptions.StartAndExpand,
                           IsOpaque=true,
                           Aspect=Aspect.AspectFill
                       },
                   },
                },
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(10, 5, 10, 5),
            };
        }
	}
}
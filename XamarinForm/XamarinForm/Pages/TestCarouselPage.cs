using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm.Pages
{
    public class TestCarouselPage : CarouselPage
    {
        public TestCarouselPage()
        {
            ToolbarItems.Add(new ToolbarItem("Add", "", () =>{ }) { Order = ToolbarItemOrder.Primary });
            Title = "TestCarouselPage";
            Color[] colors = { Color.Red, Color.Green, Color.Blue };
            foreach (Color c in colors)
            {
                Children.Add(new ContentPage
                {
                    Content = new StackLayout
                    {
                        Children = {
                            new Label { Text = c.ToString () },
                            new BoxView {
                                Color = c,
                                VerticalOptions = LayoutOptions.FillAndExpand
                            }
                        }
                    }
                });
            }
        }
    }
}
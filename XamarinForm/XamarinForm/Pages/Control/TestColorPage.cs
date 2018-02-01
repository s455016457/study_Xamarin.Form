using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Control
{
    public class TestColorPage : ControlBasePage
    {
        public TestColorPage() : base()
        {
            Title = "颜色列表";
        }
        protected override View GetView()
        {
            StackLayout stackLayout = new StackLayout()
            {
                Orientation=StackOrientation.Vertical,
                HorizontalOptions=LayoutOptions.Fill,
                VerticalOptions=LayoutOptions.CenterAndExpand,
            };

            Type colorType = typeof(Color);

            foreach (FieldInfo fif in colorType.GetFields())
            {
                if (fif.FieldType.Equals(typeof(Color)))
                {
                    stackLayout.Children.Add(new StackLayout
                    {
                        Orientation=StackOrientation.Horizontal,
                        HorizontalOptions=LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Margin=new Thickness(10,2,0,2),
                        Children = {
                            new BoxView{
                                BackgroundColor=(Color)fif.GetValue(null) ,
                                HeightRequest=30,
                                WidthRequest=40
                            },
                            new Label{
                                Text=fif.Name,
                                VerticalOptions=LayoutOptions.CenterAndExpand,
                            }
                        }
                    });
                }
            }

            return stackLayout;
        }
    }
}

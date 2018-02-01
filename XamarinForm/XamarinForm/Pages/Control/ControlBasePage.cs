using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Control
{
    public abstract class ControlBasePage:ContentPage
    {
        public ControlBasePage()
        {
            Title = "控件";
            Content = new ScrollView()
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                Content = GetView()
            };
        }

        protected abstract View GetView();
    }
}

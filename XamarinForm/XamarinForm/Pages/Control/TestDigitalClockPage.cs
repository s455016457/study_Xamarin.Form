using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Views;

namespace XamarinForm.Pages.Control
{
    public class TestDigitalClockPage:ContentPage
    {
        public TestDigitalClockPage()
        {
            Title = "电子表";
            Content = new DigitalClockView();
        }
    }
}

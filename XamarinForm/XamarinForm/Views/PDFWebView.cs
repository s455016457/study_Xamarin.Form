using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    public class PDFWebView : WebView
    {
        protected BindableProperty UrlProperty = BindableProperty.Create("Url", typeof(string), typeof(PDFWebView), defaultValue: default(string));

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
    }
}

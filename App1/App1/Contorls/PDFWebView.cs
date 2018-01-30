using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.Contorls
{
    public class PDFWebView : WebView
    {
        protected static BindableProperty UrlProperty = BindableProperty.Create("Url", typeof(string), typeof(PDFWebView), defaultValue: default(string));

        public String Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Views;

namespace XamarinForm.Pages.Control
{
    public class TestPDFWebViewPage:ContentPage
    {
        public TestPDFWebViewPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new PDFWebView {
                        Url= "Load_Pdf_in_WebView.pdf",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }
            };
        }
    }
}

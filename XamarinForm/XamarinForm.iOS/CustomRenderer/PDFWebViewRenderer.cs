using Foundation;
using System.IO;
using System.Net;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForm.iOS.CustomRenderer;
using XamarinForm.Views;

[assembly:ExportRenderer(typeof(PDFWebView),typeof(PDFWebViewRenderer))]
namespace XamarinForm.iOS.CustomRenderer
{
    class PDFWebViewRenderer: ViewRenderer<PDFWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<PDFWebView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }

            if (e.OldElement == null)
            {
                //Cleanup
            }
            if (e.NewElement != null)
            {
                var pdfWebView = Element as PDFWebView;
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(pdfWebView.Url)));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName,false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Contorls;
using App1.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(PDFWebView),typeof(PDFWebViewRenderer))]
namespace App1.Droid.Renderers
{
    class PDFWebViewRenderer : WebViewRenderer
    {
        public PDFWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            var pdfWebView = Element as PDFWebView;
            Control.Settings.AllowUniversalAccessFromFileURLs = true;
            Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}"
                , string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(pdfWebView.Url))));
        }
    }
}
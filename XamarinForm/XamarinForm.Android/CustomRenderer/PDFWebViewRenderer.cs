﻿
using System.Net;
using Android.Content;
using XamarinForm.Views;
using XamarinForm.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(PDFWebView), typeof(PDFWebViewRenderer))]
namespace XamarinForm.Droid.CustomRenderer
{
    public class PDFWebViewRenderer : WebViewRenderer
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
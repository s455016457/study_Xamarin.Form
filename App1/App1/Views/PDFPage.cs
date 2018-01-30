using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App1.Views
{
	public class PDFPage : ContentPage
	{
		public PDFPage ()
        {
            Title = "spec.pdf";
            Content = new StackLayout {
				Children = {
					new Contorls.PDFWebView{
                        Url ="spec.pdf",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    }
				}
			};
		}
	}
}
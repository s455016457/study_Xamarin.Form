using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.Utilities;

namespace XamarinForm
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            //MainPage = new XamarinForm.MainPage();
            //IList<ToolbarItem> toolbarItem = new List<ToolbarItem>();
            MainPage = new NavigationPage(new MyMasterDetailPage()) /*{ BarBackgroundColor = Color.Green, BarTextColor = Color.Blue,Title="示例APP" }*/;
            //MainPage = new NavigationPage(new MainPage_Test());
        }

        protected override void OnStart()
        {
        }

		protected override void OnSleep ()
		{
        }

		protected override void OnResume ()
        {
        }
	}
}

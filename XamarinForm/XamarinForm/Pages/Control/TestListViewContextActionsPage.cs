using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Models;
using XamarinForm.Services;
using XamarinForm.Views;

namespace XamarinForm.Pages.Control
{
    public class TestListViewContextActionsPage :ContentPage
    {
        IList<DataItem> items;

        IDataStore<DataItem> dataStore = new MockDataStore();
        public TestListViewContextActionsPage()
        {
            items = dataStore.GetItemsAsync().Result;
            //ListViewContextActionsCell cell = new ListViewContextActionsCell();

            ListView listView = new ListView()
            {
                ItemTemplate = new DataTemplate(typeof(ListViewContextActionsCell)),
                ItemsSource = items,
                RowHeight=80,
            };
            

            listView.Refreshing += ListView_Refreshing;

            Content = listView;
        }
        
        private void ListView_Refreshing(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put your refreshing logic here
            var itemList = items.Reverse().ToList();

            items.Clear();

            foreach (var s in itemList)
            {
                items.Add(s);
            }
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }
    }
}

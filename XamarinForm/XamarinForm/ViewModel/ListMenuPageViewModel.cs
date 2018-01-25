using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.ViewModel
{
    public class ListMenuPageViewModel : INotifyPropertyChanged
    {
        public String Title { get; private set; }
        public ObservableCollection<Models.MenuItem> MenuItemList { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ListMenuPageViewModel(Models.MenuItem menuItem)
        {
            Title = menuItem.Title;
            MenuItemList = new ObservableCollection<Models.MenuItem>();
            if (menuItem.ChildrenMenu != null)
            {
                foreach (var item in menuItem.ChildrenMenu)
                {
                    MenuItemList.Add(item);
                }
            }
        }

    }
}
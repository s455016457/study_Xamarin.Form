using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    public class ListViewContextActionsCell : ViewCell
    {
        
        public ListViewContextActionsCell()
        {
            Grid grid = new Grid
            {
                ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Star } },
            };

            Label label_id = new Label { Text = "ID：" };
            Label id = new Label { VerticalOptions = LayoutOptions.CenterAndExpand, FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) };

            id.SetBinding(Label.TextProperty, new Binding("MenuItemId"));

            Label label_Title = new Label { Text = "Title：" };
            Label Title = new Label { VerticalOptions = LayoutOptions.CenterAndExpand, FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) };
            Title.SetBinding(Label.TextProperty, new Binding("Title"));

            Label label_Description = new Label { Text = "Description：" };
            Label Description = new Label { VerticalOptions = LayoutOptions.CenterAndExpand, FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)) };
            Description.SetBinding(Label.TextProperty, new Binding("Description"));

            grid.Children.Add(label_id, 0, 0);
            grid.Children.Add(id, 1, 0);
            grid.Children.Add(label_Title, 0, 1);
            grid.Children.Add(Title, 1, 1);
            grid.Children.Add(label_Description, 0, 2);
            grid.Children.Add(Description, 1, 2);

            MenuItem moreAction = new MenuItem { Text = "更多" };
            moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            moreAction.Clicked += MoreActionClicked;

            MenuItem removeAction = new MenuItem { Text = "删除" };
            removeAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            removeAction.Clicked += RemoveActionClicked;

            ContextActions.Add(moreAction);
            ContextActions.Add(removeAction);

            View = grid;
        }
        private void RemoveActionClicked(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
        }

        private void MoreActionClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.MenuItem)sender;
            //DisplayAlert("更多 Context Action", item.CommandParameter + " 更多 context action", "OK");
        }

    }
}

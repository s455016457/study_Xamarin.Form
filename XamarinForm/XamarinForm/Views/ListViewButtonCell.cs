using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    public class ListViewButtonCell:ViewCell
    {
        View _leftView,_rightView;
        public ListViewButtonCell(View view,params Button[] buttons)
        {
            Grid grid = new Grid();
            view.HorizontalOptions = LayoutOptions.StartAndExpand;
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions=LayoutOptions.End,
            };
            stackLayout.Children.Add(view);
            if (buttons != null)
            {
                foreach (Button button in buttons)
                {
                    stackLayout.Children.Add(button);
                }
            }

            _leftView = view;
            _rightView = stackLayout;
            grid.Children.Add(stackLayout, 0, 0);
            grid.Children.Add(view, 0, 0);
            this.View = grid;
        }
        //监听左滑事件，并使View向左移动
    }
}

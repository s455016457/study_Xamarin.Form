using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForm.Views
{
    public class GridMenu : ScrollView
    {
        private int _columnDefinition = 4;
        private Grid grid;
        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnDefinition {
            get { return _columnDefinition; } 
            set {
                if (value <= 0)
                    throw new Exception("列数不能小于1");
                _columnDefinition = value;
            }
        }
        private IEnumerable<Models.MenuItem> _items;
        public GridMenu()
        {
            Padding = new Thickness(5, 5, 5, 5);
            grid = new Grid {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            var length = new GridLength(20,GridUnitType.Absolute);
            //grid.RowSpacing = length.Value;
            grid.ColumnSpacing = length.Value;
            IsClippedToBounds = true;
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Fill;
            Content = grid;
        }

        public void BindData(IEnumerable<Models.MenuItem>  Items)
        {
            _items = Items;
        }

        public void Renderer()
        {
            double columnWidth = 60;
            VisualElement visualElement = Parent as VisualElement;
            if (visualElement != null)
            {
                columnWidth = visualElement.Width / ColumnDefinition;
            }

            while (grid.Children.Count > 0)
            {
                grid.Children.RemoveAt(0);
            }

            for (int i = 0; i < ColumnDefinition; i++) {
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Star
                });
            }

            for (int i = 0; i < Math.Round(((decimal)_items.Count()) / ColumnDefinition); i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(columnWidth + 15)
                });
            }

            int rowIndex = 0, column_index = 0;
            foreach (Models.MenuItem item in _items)
            {
                grid.Children.Add(new GridMenuItem(item),column_index++,rowIndex);
                if (column_index == ColumnDefinition)
                {
                    column_index = 0;
                    rowIndex++;
                }
            }
        }
	}
}
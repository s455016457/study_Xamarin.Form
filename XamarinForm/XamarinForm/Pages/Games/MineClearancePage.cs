using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.Games;
using XamarinForm.Extensions;

namespace XamarinForm.Pages.Games
{
	public class MineClearancePage : ContentPage
	{
        Entry rowEntry, columnEntry, mineEntry;
        Button initButton, newGarmeButton, victoryButton, defeatedButton;
        Board board;

        public MineClearancePage ()
		{
            Title = "扫雷";
            Grid grid = new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition{ Width=GridLength.Star},
                    new ColumnDefinition{ Width=GridLength.Star},
                    new ColumnDefinition{ Width=GridLength.Star},
                    new ColumnDefinition{ Width=GridLength.Star},
                    new ColumnDefinition{ Width=GridLength.Star},
                    new ColumnDefinition{ Width=GridLength.Star},
                },
                RowDefinitions = {
                    new RowDefinition{ Height=GridLength.Auto},
                    new RowDefinition{ Height=GridLength.Auto},
                    new RowDefinition{ Height=GridLength.Auto},
                    new RowDefinition{ Height=GridLength.Auto},
                    new RowDefinition{ Height=GridLength.Auto},
                    new RowDefinition{ Height=GridLength.Star},
                }
            };

            Padding = new Thickness(5);

            rowEntry = new Entry
            {
                Text = "8",
                Keyboard = Keyboard.Numeric,
                Placeholder = "请输入大于0的行数",
                PlaceholderColor=Color.Gray,
            };

            columnEntry = new Entry
            {
                Text = "8",
                Keyboard = Keyboard.Numeric,
                Placeholder = "请输入大于0的列数",
                PlaceholderColor = Color.Gray,
            };

            mineEntry = new Entry
            {
                Text = "10",
                Keyboard = Keyboard.Numeric,
                Placeholder = "请输入大于0的地雷数量",
                PlaceholderColor = Color.Gray,
            };

            initButton = new Button
            {
                Text = "初始化游戏",
            };
            initButton.Clicked += InitButton_Clicked;

            newGarmeButton = new Button
            {
                Text = "重新开始游戏",
            };

            newGarmeButton.Clicked += NewGarmeButton_Clicked;

            Label message = new Label
            {
                Text="单击标记或取消标记，双击扫雷，第一次双击永远不会中地雷",
                TextColor=Color.Gray,
                FontSize=Device.GetNamedSize(NamedSize.Micro,typeof(Label)),
            };

            board = new Board();

            BindingContext = board;

            board.onDefeated += Board_onDefeated;
            board.onVictory += Board_onVictory;

            victoryButton = new Button
            {
                Text = "胜利",
                TextColor = Color.DarkRed,
                //WidthRequest = 40,
                //HeightRequest = 30,
                VerticalOptions=LayoutOptions.Center,
                HorizontalOptions=LayoutOptions.Center,
                BorderWidth=2,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                IsVisible = false,
                IsEnabled = false,
            };
            victoryButton.Clicked += NewGarmeButton_Clicked;

            defeatedButton = new Button
            {
                Text = "失败",
                TextColor = Color.Red,
                //WidthRequest=40,
                //HeightRequest=30,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BorderWidth = 2,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                IsVisible = false,
                IsEnabled = false,
        };
            defeatedButton.Clicked += NewGarmeButton_Clicked;
            
            Label unExposedTileLable = new Label
            {
                FontSize=Device.GetNamedSize(NamedSize.Micro,typeof(Label)),
            };
            unExposedTileLable.SetBinding(Label.TextProperty, "UnExposedTile", stringFormat: "未暴露雷区数量{0}");

            Label unFlaggedTileCountLable = new Label
            {
                BindingContext = board,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };
            unFlaggedTileCountLable.SetBinding(Label.TextProperty, "FlaggedTileCount", stringFormat: "已标记雷区数量{0}");

            Label unMineCountLable = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };
            unMineCountLable.SetBinding(Label.TextProperty, "MineCount", stringFormat: "雷区数量{0}");

            StackLayout stackLayout = new StackLayout
            {
                Orientation=StackOrientation.Horizontal,
                Children = {
                    unExposedTileLable,
                    unFlaggedTileCountLable,
                    unMineCountLable,
                },
            };

            Label gameTimeLable = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };
            gameTimeLable.SetBinding(Label.TextProperty, "GameTime", converter: new GameTimeValueConverter(), stringFormat: "已用游戏时间{0}");

            grid.Children.Add(new Label { Text="行数",}, 0, 0);
            grid.Children.Add(rowEntry, 1, 0);
            grid.Children.Add(new Label { Text="列数",}, 2,0);
            grid.Children.Add(columnEntry, 3, 0);
            grid.Children.Add(new Label { Text="地雷数量",}, 4,0);
            grid.Children.Add(mineEntry, 5, 0);
            grid.Children.Add(initButton, 0, 1);
            grid.Children.Add(newGarmeButton, 3, 1);
            grid.Children.Add(message, 0, 2);
            grid.Children.Add(gameTimeLable, 0, 3);
            grid.Children.Add(stackLayout, 0, 4);
            grid.Children.Add(board, 0, 5);
            grid.Children.Add(victoryButton, 0, 5);
            grid.Children.Add(defeatedButton, 0, 5);
            
            Grid.SetColumnSpan(initButton, 3);
            Grid.SetColumnSpan(newGarmeButton, 3);
            Grid.SetColumnSpan(message, 6);
            Grid.SetColumnSpan(gameTimeLable, 6);
            Grid.SetColumnSpan(stackLayout, 6);
            Grid.SetColumnSpan(victoryButton, 6);
            Grid.SetColumnSpan(defeatedButton, 6);
            Grid.SetColumnSpan(board, 6);

            Content = grid;
        }

        #region 事件处理
        private async void Board_onVictory(params object[] age)
        {
            victoryButton.Scale = 0;
            victoryButton.IsVisible = true;
            victoryButton.IsEnabled = true;
            double consolationTextWidth = victoryButton.Measure(Double.PositiveInfinity, Double.PositiveInfinity).Request.Width;

            double maxScale = 0.9 * board.Width / consolationTextWidth;
            await victoryButton.ScaleTo(maxScale, 1000, Easing.BounceOut);
        }

        private async void Board_onDefeated(params object[] age)
        {
            defeatedButton.Scale = 0;
            defeatedButton.IsVisible = true;
            defeatedButton.IsEnabled = true;

            double consolationTextWidth = defeatedButton.Measure(Double.PositiveInfinity, Double.PositiveInfinity).Request.Width;

            double maxScale = 0.9 * board.Width / consolationTextWidth;
            await defeatedButton.ScaleTo(maxScale, 1000, Easing.BounceOut);
        }

        private void NewGarmeButton_Clicked(object sender, EventArgs e)
        {
            victoryButton.IsVisible = false;
            victoryButton.IsEnabled = false;
            defeatedButton.IsVisible = false;
            defeatedButton.IsEnabled = false;
            board.NewGame();
        }

        private void InitButton_Clicked(object sender, EventArgs e)
        {
            int rowCount = 0, columnCount = 0, mineCount = 0;
            
            if (!int.TryParse(rowEntry.Text.Trim(), out rowCount) || rowCount < 0)
            {
                rowEntry.BackgroundColor = Color.Red;
                DisplayAlert("温馨提示", "请输入正确的行数", "确定");
                return;
            }

            if (!int.TryParse(columnEntry.Text.Trim(), out columnCount)||columnCount<0)
            {
                columnEntry.BackgroundColor = Color.Red;
                DisplayAlert("温馨提示", "请输入正确的行数", "确定");
                return;
            }

            if (!int.TryParse(mineEntry.Text.Trim(), out mineCount) || mineCount < 0)
            {
                mineEntry.BackgroundColor = Color.Red;
                DisplayAlert("温馨提示", "请输入正确的行数", "确定");
                return;
            }
            rowEntry.BackgroundColor = Color.White;
            columnEntry.BackgroundColor = Color.White;
            mineEntry.BackgroundColor = Color.White;

            board.Initialized(rowCount, columnCount, mineCount);
        }
        #endregion
    }

    class GameTimeValueConverter : IValueConverter
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">传入值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">参数</param>
        /// <param name="culture">在转换过程中使用的文化</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!value.GetType().Equals(typeof(TimeSpan))) return null;
            if (!targetType.Equals(typeof(String))) return null;

            TimeSpan _value = (TimeSpan)value;

            int _hours = _value.Hours;
            int _minutes = _value.Minutes;
            int _seconds = _value.Seconds;
            int _milliseconds = _value.Milliseconds;

            StringBuilder msg = new StringBuilder();
            if (_hours > 0)
                msg.AppendFormat("{0}时", _hours.ToString("#,##0"));
            if (_hours > 0 || _minutes > 0)
                msg.AppendFormat("{0}分", _minutes);

            msg.AppendFormat("{0}秒", _seconds);
            msg.AppendFormat("{0}毫秒", _milliseconds);

            return msg.ToString();
        }

        /// <summary>
        /// 逆转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!value.GetType().Equals(typeof(String))) return null;
            if (!targetType.Equals(typeof(TimeSpan))) return null;
                        
            int _hours = 0,_minutes = 0, _seconds = 0, _milliseconds = 0;

            int index = 0;
            foreach (int _value in value.ToString().ExtractIntDesc())
            {
                switch (index)
                {
                    case 0:
                        _milliseconds = _value;
                        break;
                    case 1:
                        _seconds = _value;
                        break;
                    case 2:
                        _minutes = _value;
                        break;
                    case 3:
                        _hours = _value;
                        break;
                }
                index++;
            }
            
            return new TimeSpan(_hours, _minutes, _seconds, _milliseconds);
        }
    }
}
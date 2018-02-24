using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.Games;

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
                    new RowDefinition{ Height=new GridLength(5)},
                    new RowDefinition{ Height=new GridLength(5)},
                    new RowDefinition{ Height=GridLength.Star},
                }
            };

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

            grid.Children.Add(new Label { Text="行数",}, 0, 0);
            grid.Children.Add(rowEntry, 1, 0);
            grid.Children.Add(new Label { Text="列数",}, 2,0);
            grid.Children.Add(columnEntry, 3, 0);
            grid.Children.Add(new Label { Text="地雷数量",}, 4,0);
            grid.Children.Add(mineEntry, 5, 0);
            grid.Children.Add(initButton, 0, 1);
            grid.Children.Add(newGarmeButton, 3, 1);
            grid.Children.Add(message, 0, 2);
            grid.Children.Add(board, 0, 5);
            grid.Children.Add(victoryButton, 0, 5);
            grid.Children.Add(defeatedButton, 0, 5);
            
            Grid.SetColumnSpan(initButton, 3);
            Grid.SetColumnSpan(newGarmeButton, 3);
            Grid.SetColumnSpan(message, 6);
            Grid.SetColumnSpan(victoryButton, 6);
            Grid.SetColumnSpan(defeatedButton, 6);
            Grid.SetColumnSpan(board, 6);

            Content = grid;
        }

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
    }
}
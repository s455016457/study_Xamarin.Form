using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    public class DigitalClockView: AbsoluteLayout
    {
        public BindableProperty ColorOnProperty = BindableProperty.Create("colorOn", typeof(Color), typeof(DigitalClockView), Color.Black);
        public BindableProperty ColorOffProperty = BindableProperty.Create("colorOff ", typeof(Color), typeof(DigitalClockView), new Color(0.5, 0.5, 0.5, 0.25));
        
        /// <summary>
        /// 钟表字体颜色
        /// </summary>
        public Color ColorOn
        {
            get { return (Color)GetValue(ColorOnProperty); }
            set { SetValue(ColorOnProperty, value); }
        }
        /// <summary>
        /// 钟表背景颜色
        /// </summary>
        public Color ColorOff
        {
            get { return (Color)GetValue(ColorOffProperty); }
            set { SetValue(ColorOffProperty, value); }
        }

        // 水平点数.
        const int horzDots = 41;
        // 垂直点数
        const int vertDots = 7;

        //数字0到9的5 x 7点矩阵的模式
        static readonly int[,,] numberPatterns = new int[10, 7, 5]
        {
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 1, 1}, { 1, 0, 1, 0, 1},
                { 1, 1, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 0, 1, 0, 0}, { 0, 1, 1, 0, 0}, { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0},
                { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 1, 0},
                { 0, 0, 1, 0, 0}, { 0, 1, 0, 0, 0}, { 1, 1, 1, 1, 1}
            },
            {
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 1, 0}, { 0, 0, 1, 0, 0}, { 0, 0, 0, 1, 0},
                { 0, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 0, 0, 1, 0}, { 0, 0, 1, 1, 0}, { 0, 1, 0, 1, 0}, { 1, 0, 0, 1, 0},
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 1, 0}, { 0, 0, 0, 1, 0}
            },
            {
                { 1, 1, 1, 1, 1}, { 1, 0, 0, 0, 0}, { 1, 1, 1, 1, 0}, { 0, 0, 0, 0, 1},
                { 0, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 0, 1, 1, 0}, { 0, 1, 0, 0, 0}, { 1, 0, 0, 0, 0}, { 1, 1, 1, 1, 0},
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 1, 0}, { 0, 0, 1, 0, 0},
                { 0, 1, 0, 0, 0}, { 0, 1, 0, 0, 0}, { 0, 1, 0, 0, 0}
            },
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0},
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            {
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1}, { 0, 0, 0, 1, 0}, { 0, 1, 1, 0, 0}
            },
        };

        // 冒号的点矩阵的模式.
        static readonly int[,] colonPattern = new int[7, 2]
        {
            { 0, 0 }, { 1, 1 }, { 1, 1 }, { 0, 0 }, { 1, 1 }, { 1, 1 }, { 0, 0 }
        };

        //6位数字的框视图，7行，5列。
        BoxView[,,] digitBoxViews = new BoxView[6, 7, 5];

        public DigitalClockView()
        {
            VerticalOptions = LayoutOptions.Center;
            Margin = new Thickness(5, 5, 5, 5);
            // BoxView 的大小.
            double width = 0.85 / (horzDots+4+7);
            double height = 0.85 / vertDots;

            // 创建并组装BoxViews.
            double xIncrement = 1.0 / (horzDots - 1);
            double yIncrement = 1.0 / (vertDots - 1);
            double x = 0;
            for (int digit = 0; digit < 6; digit++)
            {
                for (int col = 0; col < 5; col++)
                {
                    double y = 0;

                    for (int row = 0; row < 7; row++)
                    {
                        // 创建数字BoxView并添加到布局中。
                        BoxView boxView = new BoxView();
                        digitBoxViews[digit, row, col] = boxView;
                        Children.Add(boxView,new Rectangle(x, y, width, height),AbsoluteLayoutFlags.All);
                        y += yIncrement;
                    }
                    x += xIncrement;
                }
                x += xIncrement;

                // 时分秒之间的冒号。
                if (digit == 1 || digit == 3)
                {
                    int colon = digit / 2;

                    for (int col = 0; col < 2; col++)
                    {
                        double y = 0;

                        for (int row = 0; row < 7; row++)
                        {
                            // 创建 BoxView 并设置它的演示.
                            BoxView boxView = new BoxView
                            {
                                Color = colonPattern[row, col] == 1 ? ColorOn : ColorOff
                            };
                            Children.Add(boxView,new Rectangle(x, y, width, height),AbsoluteLayoutFlags.All);
                            y += yIncrement;
                        }
                        x += xIncrement;
                    }
                    x += xIncrement;
                }
            }

            // 设置计时器并手动调用进行初始化
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimer);
            OnTimer();
        }
        void OnPageSizeChanged(object sender, EventArgs args)
        {
            // 显示器的横高比为 52：7
            HeightRequest = vertDots * Width / (horzDots+4+7);
        }
        bool OnTimer()
        {
            DateTime dateTime = DateTime.Now;

            // 将24小时时钟转换为12小时时钟。
            int hour = (dateTime.Hour + 11) % 12 + 1;

            // 设置每个数字在显示器上显示
            SetDotMatrix(0, hour / 10);
            SetDotMatrix(1, hour % 10);
            SetDotMatrix(2, dateTime.Minute / 10);
            SetDotMatrix(3, dateTime.Minute % 10);
            SetDotMatrix(4, dateTime.Second / 10);
            SetDotMatrix(5, dateTime.Second % 10);
            return true;
        }

        /// <summary>
        /// 设置数字显示
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="digit">数字</param>
        void SetDotMatrix(int index, int digit)
        {
            for (int row = 0; row < 7; row++)
                for (int col = 0; col < 5; col++)
                {
                    bool isOn = numberPatterns[digit, row, col] == 1;
                    Color color = isOn ? ColorOn : ColorOff;
                    digitBoxViews[index, row, col].Color = color;
                }
        }
    }
}

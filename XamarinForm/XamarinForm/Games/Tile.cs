using System;
using Xamarin.Forms;

namespace XamarinForm.Games
{
    public enum TileStatus
    {
        /// <summary>
        /// 隐藏
        /// </summary>
        Hidden,
        /// <summary>
        /// 标记
        /// </summary>
        Flagged,
        /// <summary>
        /// 暴露
        /// </summary>
        Exposed
    }

    public class Tile : Frame
    {
        TileStatus tileStatus = TileStatus.Hidden;
        Label label;
        Label biaojiLabel, mineLabel;
        Image mineImage,flagImage;
        static ImageSource flagImageSource,mineImageSource;
        bool doNotFireEvent;

        public event EventHandler<TileStatus> TileStatusChanged;

        static Tile()
        {
            flagImageSource = ImageSource.FromFile("Xamarin120.png");
            mineImageSource = ImageSource.FromFile("RedBug.png");
        }
        public Tile(int Row,int Column)
        {
            this.Row = Row;
            this.Column = Column;
            Status = TileStatus.Hidden;
            BackgroundColor = Color.Yellow;
            OutlineColor = Color.Blue;
            Padding = 1;
            
            label = new Label
            {
                Text = " ",
                TextColor = Color.Yellow,
                BackgroundColor = Color.Blue,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment= TextAlignment.Center,
            };

            mineImage = new Image
            {
                Source=mineImageSource,
            };

            flagImage = new Image
            {
                Source= flagImageSource,
            };

            biaojiLabel = new Label { Text = "标", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            mineLabel = new Label { Text = "雷", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

            TapGestureRecognizer singleTPG = new TapGestureRecognizer
            {
                NumberOfTapsRequired=1,
            };

            singleTPG.Tapped += SingleTPG_Tapped;
            GestureRecognizers.Add(singleTPG);
#if FIX_WINDOWS_DOUBLE_TAPS
            if (Device.RuntimePlatform != Device.UWP && Device.RuntimePlatform != Device.WinPhone) {
#endif
            TapGestureRecognizer doubleTpg = new TapGestureRecognizer
            {
                NumberOfTapsRequired=2,
            };
            doubleTpg.Tapped += DoubleTpg_Tapped;
            GestureRecognizers.Add(doubleTpg);
#if FIX_WINDOWS_DOUBLE_TAPS
            }
#endif
        }

#if FIX_WINDOWS_DOUBLE_TAPS

        bool lastTapSingle;
        DateTime lastTapTime;

#endif
        private void SingleTPG_Tapped(object sender, System.EventArgs e)
        {
#if FIX_WINDOWS_DOUBLE_TAPS
            
            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone) {
                if (lastTapSingle && DateTime.Now - lastTapTime < TimeSpan.FromMilliseconds (500)) {
                    OnDoubleTap (sender, args);
                    lastTapSingle = false;
                } else {
                    lastTapTime = DateTime.Now;
                    lastTapSingle = true;
                }
        	}

#endif
            switch (Status) {
                case TileStatus.Hidden:
                    Status = TileStatus.Flagged;
                    break;
                case TileStatus.Flagged:
                    Status = TileStatus.Hidden;
                    break;
                case TileStatus.Exposed:
                    //什么也不做
                    break;
            }
        }

        private void DoubleTpg_Tapped(object sender, System.EventArgs e)
        {
            Status = TileStatus.Exposed;
        }

        /// <summary>
        /// 地雷
        /// </summary>
        public bool IsMine { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public TileStatus Status
        {
            get
            {
                return tileStatus;
            }
            set
            {
                if (tileStatus == value)
                    return;
                tileStatus = value;

                switch (tileStatus)
                {
                    case TileStatus.Hidden:
                        Content = null;

#if FIX_WINDOWS_PHONE_NULL_CONTENT
                       if (Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.UWP) {
                           this.Content = new Label { Text = " " };
                       }
#endif
                        break;
                    case TileStatus.Flagged:
                        Content = flagImage;
                        //Content = biaojiLabel;
                        break;
                    case TileStatus.Exposed:
                        if (IsMine)
                            Content = mineImage;
                        //Content = mineLabel;
                        else {
                            Content = label;
                            label.Text = SurroundingMineCount > 0 ? SurroundingMineCount.ToString() : " ";
                        }
                        break;
                }
                if (!doNotFireEvent && TileStatusChanged != null)
                {
                    TileStatusChanged(this, tileStatus);
                }
            }
        }
        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// 周围地雷数量
        /// </summary>
        public int SurroundingMineCount { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            doNotFireEvent = true;
            Status = TileStatus.Hidden;
            IsMine = false;
            SurroundingMineCount = 0;
            doNotFireEvent = false;
        }
    }
}

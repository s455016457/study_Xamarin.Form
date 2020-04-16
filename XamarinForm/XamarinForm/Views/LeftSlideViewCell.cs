using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    /// <summary>
    /// 左滑视图
    /// </summary>
    public abstract class LeftSlideViewCell : ViewCell
    {
        /// <summary>
        /// 左滑时触发
        /// </summary>
        event EventHandler OnLeftSlide;
        /// <summary>
        /// 左边滑动视图
        /// </summary>
        public LeftSlideContentView LeftView { get; private set; }
        /// <summary>
        /// 右边视图
        /// </summary>
        public View RightView { get; private set; }
        /// <summary>
        /// 已滑动
        /// </summary>
        public bool IsSlide { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="leftView">左边滑动视图</param>
        /// <param name="rightView">右边视图</param>
        public LeftSlideViewCell()
        {
            Grid grid = new Grid()
            {
                BackgroundColor=Color.White,
            };

            grid.RowDefinitions.Add(new RowDefinition { Height=GridLength.Auto,});

            LeftView = CreateLeftView();
            RightView = CreateRightView();
            LeftView.OnReleased += LeftView_OnReleased;

            if(RightView.HeightRequest< LeftView.Height)
                RightView.HeightRequest = LeftView.Height;
            else
                LeftView.HeightRequest = RightView.Height;

            LeftView.VerticalOptions = LayoutOptions.FillAndExpand;
            LeftView.HorizontalOptions = LayoutOptions.FillAndExpand;

            RightView.HorizontalOptions = LayoutOptions.End;
            RightView.VerticalOptions = LayoutOptions.CenterAndExpand;

            grid.Padding = new Thickness(10, 5, 5, 5);

            grid.Children.Add(RightView, 0, 0);
            grid.Children.Add(LeftView, 0, 0);

            //TapGestureRecognizer singleTPG = new TapGestureRecognizer
            //{
            //    NumberOfTapsRequired = 1,
            //};

            //singleTPG.Tapped += SingleTPG_Tapped;

            //grid.GestureRecognizers.Add(singleTPG);

            grid.Focused += Grid_Focused;

            View = grid;
        }

        private void Grid_Focused(object sender, FocusEventArgs e)
        {
            if (IsSlide)
                UnSlideAsync();
        }

        private void SingleTPG_Tapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建右边视图
        /// </summary>
        /// <returns></returns>
        public abstract View CreateRightView();
        /// <summary>
        /// 创建左边视图
        /// </summary>
        /// <returns></returns>
        public abstract LeftSlideContentView CreateLeftView();
        
        /// <summary>
        /// 取消滑动
        /// </summary>
        public async void UnSlideAsync()
        {
            if (IsSlide)
            {
                await LeftView.TranslateTo(0, LeftView.TranslationY, 500);
                IsSlide = false;
            }
        }

        private async void LeftView_OnReleased(object sender, EventArg.SlideViewReleasedEventArg e)
        {
            if (e.DistanceX < -1 * RightView.Width / 2)
            {
                await LeftView.TranslateTo(-1 * RightView.Width, LeftView.TranslationY, 100);
                //LeftView.TranslationX = -1 * RightView.Width;
                IsSlide = true;

                if (OnLeftSlide != null)
                    OnLeftSlide.Invoke(this, new EventArgs());
            }
            else
            {
                await LeftView.TranslateTo(0, LeftView.TranslationY, 100);
                //LeftView.TranslationX = 0;
                IsSlide = false;
            }
        }
    }
}
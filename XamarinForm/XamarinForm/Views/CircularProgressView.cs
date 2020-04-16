using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Views
{
    /// <summary>
    /// 环形进度条
    /// </summary>
    public class CircularProgressView : Grid
    {
        View progress1;
        View progress2;
        View background1;
        View background2;
        public static BindableProperty ProgressProperty = BindableProperty.Create("Progress", typeof(double), typeof(CircularProgressView), 0d, propertyChanged: ProgressChanged);

        public CircularProgressView()
        {
            //WidthRequest = 30;
            //HeightRequest = 30;
            progress1 = CreateImage("progress_done");
            background1 = CreateImage("progress_pending");
            background2 = CreateImage("progress_pending");
            progress2 = CreateImage("progress_done");
            //初始化图片位置
            HandleProgressChanged(1, 0);
        }
        private View CreateImage(string v1)
        {
            var img = new Image();
            img.Source = ImageSource.FromFile(v1 + ".png");
            this.Children.Add(img);
            return img;
        }

        private static void ProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var c = bindable as CircularProgressView;
            c.HandleProgressChanged(Clamp((double)oldValue, 0, 1), Clamp((double)newValue, 0, 1));
        }

        static double Clamp(double value, double min, double max)
        {
            if (value <= max && value >= min) return value;
            else if (value > max) return max;
            else return min;
        }

        private void HandleProgressChanged(double oldValue, double p)
        {
            if (p < .5)
            {
                if (oldValue >= .5)
                {
                    background1.IsVisible = true;
                    progress2.IsVisible = false;
                    background2.Rotation = 180;
                    progress1.Rotation = 0;
                }
                double rotation = 360 * p;
                background1.Rotation = rotation;
            }
            else
            {
                if (oldValue < .5)
                {
                    background1.IsVisible = false;
                    progress2.IsVisible = true;
                    progress1.Rotation = 180;
                }
                double rotation = 360 * p;
                background2.Rotation = rotation;
            }
        }

        /// <summary>
        /// 进度属性
        /// </summary>
        public double Progress
        {
            get { return (double)this.GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }
    }
}

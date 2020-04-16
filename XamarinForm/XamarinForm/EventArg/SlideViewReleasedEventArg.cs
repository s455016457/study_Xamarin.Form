using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.EventArg
{
    public class SlideViewReleasedEventArg : EventArgs
    {
        /// <summary>
        /// 滑动距离X
        /// </summary>
        public double DistanceX;

        /// <summary>
        /// 滑动距离Y
        /// </summary>
        public double DistanceY;

        public SlideViewReleasedEventArg(double DistanceX, double DistanceY)
        {
            this.DistanceX = DistanceX;
            this.DistanceY = DistanceY;
        }
    }
}

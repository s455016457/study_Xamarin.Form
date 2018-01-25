using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.CustomEffect.TouchTrack
{
    /// <summary>
    /// 触摸事件参数
    /// </summary>
    public class TouchActionEventArgs:EventArgs
    {
        public TouchActionEventArgs(long id,TouchActionType type,Point location,Boolean isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }
        /// <summary>
        /// 事件ID
        /// </summary>
        public long Id { get; private set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public TouchActionType Type { get; private set; }
        /// <summary>
        /// 位置
        /// </summary>
        public Point Location { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean IsInContact { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.CustomEffect.TouchTrack
{
    public enum TouchActionType
    {
        /// <summary>
        /// 进入
        /// </summary>
        Entered,
        /// <summary>
        /// 按压
        /// </summary>
        Pressed,
        /// <summary>
        /// 移动
        /// </summary>
        Moved,
        /// <summary>
        /// 释放
        /// </summary>
        Released,
        /// <summary>
        /// 退出
        /// </summary>
        Exited,
        /// <summary>
        /// 取消
        /// </summary>
        Cancelled
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.DependencyServices.MyNotification
{
    /// <summary>
    /// 通知
    /// </summary>
    public class NotificationConfig
    {
        /// <summary>
        /// 默认是否震动
        /// 默认为True
        /// </summary>
        public static Boolean DefaultIsVibreate { get; set; } = true;
        /// <summary>
        /// 默认是否提示
        /// 默认为True
        /// </summary>
        public static Boolean DefaultIsSound { get; set; } = true;

        /// <summary>
        /// 消息ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 副标题
        /// IOS支持
        /// Android不支持
        /// </summary>
        public String Subtitle { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public String Message { get; set; }
        /// <summary>
        /// 徽章
        /// 在桌面图标上显示的通知数量
        /// </summary>
        public int Badge { get; set; }
        /// <summary>
        /// 是否震动
        /// </summary>
        public Boolean IsVibrate { get; set; } = DefaultIsVibreate;
        /// <summary>
        /// 是否提示
        /// </summary>
        public Boolean IsSound { get; set; } = DefaultIsSound;
        /// <summary>
        /// 震动模式
        /// 例如 [500,500]
        /// </summary>
        public long[] VibratePattern { get; set; }
        /// <summary>
        /// 提示声音
        /// 提示声音文件地址
        /// </summary>
        public String SoundPath { get; set; }
    }
}

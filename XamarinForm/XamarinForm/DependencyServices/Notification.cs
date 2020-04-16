using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.DependencyServices
{
    /// <summary>
    /// 通知
    /// </summary>
    public class Notification
    {
        public Notification() { }

        /// <summary>
        /// 通知标题
        /// </summary>
        public static string DefaultTitle { get; set; }
        /// <summary>
        /// 通知提醒声音
        /// </summary>
        public static string DefaultSound { get; set; }
        /// <summary>
        /// 通知ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; } = DefaultTitle;
        /// <summary>
        /// 通知正文
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 通知提醒声音
        /// </summary>
        public string Sound { get; set; } = DefaultSound;
        /// <summary>
        /// 元数据
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 震动
        /// </summary>
        public bool Vibrate { get; set; }
        /// <summary>
        /// 延迟时间
        /// </summary>
        public TimeSpan? When { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// 已排程
        /// </summary>
        public bool IsScheduled => this.Date != null || this.When != null;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get
            {
                if (this.Date != null)
                    return this.Date.Value;

                var dt = DateTime.Now;
                if (this.When != null)
                    dt = dt.Add(this.When.Value);

                return dt;
            }
        }

        /// <summary>
        /// 设置元数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Notification SetMetadata(string key, string value)
        {
            Metadata.Add(key, value);
            return this;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForm.DependencyServices.MyNotification
{
    public interface INotificationService
    {
        /// <summary>
        /// 在IOS中请求授权
        /// </summary>
        Task<bool> RequestPermission();

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notification">消息</param>
        /// <returns></returns>
        Task Send(NotificationConfig notification);

        /// <summary>
        /// 清除通知
        /// </summary>
        /// <param name="Id">消息ID</param>
        /// <returns></returns>
        void Clear(int Id);
        /// <summary>
        /// 清除所有通知
        /// </summary>
        /// <returns></returns>
        void Clear();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForm.DependencyServices
{
    public interface INotificationService
    {
        /// <summary>
        /// 在IOS中请求授权
        /// </summary>
        Task<bool> RequestPermission();


        ///// <summary>
        ///// 获取计划通知
        ///// </summary>
        ///// <returns></returns>
        //Task<IEnumerable<Notification>> GetScheduledNotifications();


        ///// <summary>
        /////清除计划通知
        ///// </summary>
        //Task CancelAll();


        ///// <summary>
        ///// 清除特定的通知
        ///// </summary>
        ///// <param name="notificationId">通知ID</param>
        ///// <returns>如果发现并成功取消消息，返回true。</returns>
        //Task<Boolean> Cancel(int notificationId);


        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notification">通知</param>
        /// <returns>返回通知ID</returns>
        Task<int> Send(Notification notification);


        ///// <summary>
        ///// 获取当前徽章数量
        ///// </summary>
        //Task<int> GetBadge();


        ///// <summary>
        ///// 设置当前徽章数量
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //Task SetBadge(int value);


        /// <summary>
        /// 设备震动
        /// </summary>
        /// <param name="ms"></param>
        void Vibrate(int ms = 300);
    }
}

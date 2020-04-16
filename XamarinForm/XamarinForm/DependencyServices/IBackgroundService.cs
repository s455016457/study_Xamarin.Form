using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace XamarinForm.DependencyServices
{
    /// <summary>
    /// 后台服务
    /// </summary>
    public interface IBackgroundService
    {
        /// <summary>
        /// 服务开启时触发
        /// </summary>
        event EventHandler OnDoStart;
        /// <summary>
        /// 服务停止时触发
        /// </summary>
        event EventHandler OnDoStop;
        /// <summary>
        /// 服务已启动
        /// </summary>
        bool IsStart { get;}

        /// <summary>
        /// 开启服务
        /// </summary>
        void Start();
        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();
    }
}

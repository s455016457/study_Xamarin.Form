using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.Models
{
    /// <summary>
    /// 登录票据
    /// </summary>
    public class LoginBill : IDisposable
    {
        static LoginBill _this;
        static object lock_obj = new object();
        private LoginBill(String UserName, String Password, DateTime BillTime)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.BillTime = BillTime;
        }

        /// <summary>
        /// 获取当前票据
        /// </summary>
        public static LoginBill CurrentBill { get { return _this; } }

        /// <summary>
        /// 创建一个登录票据
        /// </summary>
        /// <param name="UserName">登录账号</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static LoginBill Create(String UserName, String Password)
        {
            if (String.IsNullOrWhiteSpace(UserName))
            {
                throw new Exception("登录账号不能为空！");
            }
            if (String.IsNullOrWhiteSpace(Password))
            {
                throw new Exception("登录密码不能为空！");
            }
            if (_this == null)
            {
                lock (lock_obj)
                {
                    if (_this == null)
                        _this = new LoginBill(UserName, Password, DateTime.Now);
                }
            }
            return _this;
        }
        /// <summary>
        /// 恢复登录票据
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static LoginBill Recover(String UserName, String Password, DateTime dateTime)
        {
            if (String.IsNullOrWhiteSpace(UserName) || String.IsNullOrWhiteSpace(Password))
            {
                _this = null;
                return _this;
            }
            if (_this == null)
            {
                lock (lock_obj)
                {
                    if (_this == null)
                        _this = new LoginBill(UserName, Password, dateTime);
                }
            }
            return _this;
        }

        /// <summary>
        /// 更新票据时间
        /// </summary>
        public void UpdateBillTime()
        {
            BillTime = DateTime.Now;
        }

        /// <summary>
        /// 销毁当前票据
        /// </summary>
        public void Dispose()
        {
            Dispose(true);//这样会释放所有的资源
            GC.SuppressFinalize(this);//不需要再调用本对象的Finalize方法
        }
        /// <summary>
        /// 销毁当前票据
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _this = null;
            }
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        public String UserName { get; private set; }
        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; private set; }
        /// <summary>
        /// 票据时间
        /// </summary>
        public DateTime BillTime { get; private set; }      
    }
}

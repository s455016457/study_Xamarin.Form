using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.DependencyServices
{
    /// <summary>
    /// 文件助手工厂
    /// </summary>
    public static class FileHelperFactory
    {
        /// <summary>
        /// 文件助手类
        /// </summary>
        static IFileHelper FileHelper { get; set; }
        static object lock_Obj = new object();

        /// <summary>
        /// 获取文件帮助类
        /// </summary>
        /// <returns></returns>
        public static IFileHelper GetFileHelper()
        {
            if (FileHelper != null) return FileHelper;

            lock (lock_Obj)
            {
                if (FileHelper != null) return FileHelper;

                FileHelper = DependencyService.Get<IFileHelper>();
                return FileHelper;
            }
        }
    }
}

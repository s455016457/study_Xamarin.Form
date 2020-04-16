using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.DependencyServices
{
    /// <summary>
    /// 文件助手
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// 获取本地文件路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        String GetLocalFilePath(string filename);
    }
}

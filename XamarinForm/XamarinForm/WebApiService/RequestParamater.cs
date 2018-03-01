using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using XamarinForm.Utilities;

namespace XamarinForm.WebApiService
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class RequestParamater : Dictionary<String, Object>
    {
        /// <summary>
        /// 转换成Get请求参数
        /// </summary>
        /// <returns></returns>
        public String ToParamater()
        {
            StringBuilder sb = new StringBuilder();
            ToParamater(String.Empty, this, sb);
            return sb.ToString();
        }

        private void ToParamater(String Prefix, RequestParamater paramater, StringBuilder sb)
        {
            foreach (var item in this)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                if (item.Value is RequestParamater)
                {
                    ToParamater(item.Key + "_", item.Value as RequestParamater, sb);
                }
                else
                {
                    sb.AppendFormat("{0}{1}={2}", Prefix, item.Key, System.Web.HttpUtility.UrlEncode(item.Value.ToString()));
                }
            }
        }

        /// <summary>
        /// 转换成Json格式
        /// </summary>
        /// <returns></returns>
        public String ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 创建请求参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RequestParamater Create(Dictionary<String, Object> data)
        {
            RequestParamater paramater = new RequestParamater();
            foreach (var item in data)
            {
                paramater.Add(item.Key, item.Value);
            }
            return paramater;
        }
    }
}

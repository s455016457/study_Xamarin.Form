using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using XamarinForm.Utilities;
using System.Linq;
using System.IO;
using System.Security.Cryptography;

namespace XamarinForm.WebApiService
{
    
    public class HttpRequestFactory
    {
        const String POST = "Post";
        const String GET = "Get";

        /// <summary>
        /// 创建POS请求
        /// </summary>
        /// <param name="RequestUri">请求地址</param>
        /// <param name="requestParamater">请求参数</param>
        /// <returns></returns>
        public static WebRequest CreatePostRequest(String RequestUri,RequestParamater requestParamater)
        {
            //RequestUri = "http://localhost:8082/api/Test";
            WebRequest request = WebRequest.Create(RequestUri);
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8");
            request.Method = POST;
            request.Headers.Add(HttpRequestHeader.Cookie, "Uid=admin; expires=Tue, 10 Apr 2018 10:38:54 GMT; max-age=2592000; domain=localhost; path=/; httponly");

            String appparam = requestParamater.ToJson();
            RequestAddSignature(request.Headers, appparam);

            byte[] bytes = Encoding.UTF8.GetBytes(appparam);
            request.ContentLength = bytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Flush();
            requestStream.Close();

            return request;
        }

        /// <summary>
        /// 创建Get请求
        /// </summary>
        /// <param name="RequestUri">请求地址</param>
        /// <param name="requestParamater">请求参数</param>
        /// <returns></returns>
        public static WebRequest CreateGetRequest(String RequestUri, RequestParamater requestParamater)
        {            
            RequestUri += "?" + requestParamater.ToParamater();
            WebRequest request = WebRequest.Create(RequestUri);
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8");
            request.Method = GET;
            String appparam = requestParamater.ToParamater();
            RequestAddSignature(request.Headers, appparam);

            return request;
        }


        /// <summary>
        /// 给请求添加签名
        /// </summary>
        /// <param name="header"></param>
        /// <param name="param"></param>
        private static void RequestAddSignature(WebHeaderCollection header, String param)
        {
            String timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");

            string Signature = GetSignature(AppConstant.AppKey, AppConstant.Secret, timestamp, param);
            header.Add("app_key", AppConstant.AppKey);
            header.Add("timestamp", timestamp);
            header.Add("sign", Signature);
            Console.WriteLine("app_key：{0}", AppConstant.Secret);
            Console.WriteLine("timestamp：{0}", timestamp);
            Console.WriteLine("param：{0}", param);
            Console.WriteLine("签名：{0}", Signature);
        }

        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="secret">签名字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="appparam">请求参数</param>
        /// <returns></returns>
        private static String GetSignature(String appkey, String secret, String timestamp, String appparam)
        {
            var hash = System.Security.Cryptography.MD5.Create();

            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("param_json", appparam);
            dic.Add("app_key", appkey);
            dic.Add("timestamp", timestamp);

            // 第一步：把字典按Key的字母顺序排序
            IEnumerator<KeyValuePair<String, String>> dem = dic.OrderBy(p => p.Key).GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder(secret);
            while (dem.MoveNext())
            {
                String key = dem.Current.Key;
                String value = dem.Current.Value;
                if (!String.IsNullOrWhiteSpace(key) && !String.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }
            query.Append(secret);

            // 第三步：使用MD5加密
            MD5 md5 = MD5.Create();
            Byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));

            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (Int32 i = 0; i < bytes.Length; i++)
            {
                String hex = bytes[i].ToString("X2");
                result.Append(hex);
            }

            return result.ToString();
        }
    }
}

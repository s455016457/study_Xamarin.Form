using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForm.WebApiService
{
    public class HttpClient
    {
        #region 成员方法
        /// <summary>
        /// 通过POST请求获取对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="requestUri">请求地址</param>
        /// <param name="requestParamater">请求参数</param>
        /// <returns></returns>
        public static T GetObjectByPost<T>(String requestUri, RequestParamater requestParamater) where T : class
        {
            return GetObject<T>(HttpRequestFactory.CreatePostRequest(requestUri, requestParamater) as HttpWebRequest);
        }
        /// <summary>
        /// 通过GET请求获取对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="requestUri">请求地址</param>
        /// <param name="requestParamater">请求参数</param>
        /// <returns></returns>
        public static T GetObjectByGet<T>(String requestUri, RequestParamater requestParamater) where T : class
        {
            return GetObject<T>(HttpRequestFactory.CreateGetRequest(requestUri, requestParamater) as HttpWebRequest);
        }
        /// <summary>
        /// 通过GET请求获取对象 异步
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="requestUri">请求地址</param>
        /// <param name="requestParamater">请求参数</param>
        /// <param name="callBack">回调函数</param>
        /// <returns></returns>
        public static Task<T> GetObjectByGetAsync<T>(String requestUri, RequestParamater requestParamater, Action<T> callBack) where T : class
        {
            HttpClientAsyncMethodModel<T> asyncMethodModel = new HttpClientAsyncMethodModel<T>(GetObjectByGet<T>, callBack);
            return Task<T>.Factory.FromAsync(asyncMethodModel.BeginMethod, asyncMethodModel.EndMethod, requestUri, requestParamater, null, TaskCreationOptions.None);
        }

        /// <summary>
        /// 通过POST请求获取对象 异步
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="requestUri">请求地址</param>
        /// <param name="requestParamater">请求参数</param>
        /// <param name="callBack">回调函数</param>
        /// <returns></returns>
        public static Task<T> GetObjectByPostAsync<T>(String requestUri, RequestParamater requestParamater, Action<T> callBack) where T : class
        {
            HttpClientAsyncMethodModel<T> asyncMethodModel = new HttpClientAsyncMethodModel<T>(GetObjectByPost<T>, callBack);
            return Task<T>.Factory.FromAsync(asyncMethodModel.BeginMethod, asyncMethodModel.EndMethod, requestUri, requestParamater, null, TaskCreationOptions.None);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 从Request中获取返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">请求</param>
        /// <returns></returns>
        private static T GetObject<T>(HttpWebRequest request) where T : class
        {
            String Json = GetJsonByResponse(GetResponse(request));            

            if (String.IsNullOrEmpty(Json))
            {
                return default(T);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeAnonymousType<T>(Json, default(T)) ;
        }
        

        /// <summary>
        /// 从Response中获取Json数据
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static String GetJsonByResponse(HttpWebResponse response)
        {
            StringBuilder sb = new StringBuilder();
            long total = response.ContentLength;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    String line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line);
                        Console.WriteLine(String.Format("获取响应数据：{0}", line));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取服务器响应
        /// </summary>
        /// <param name="request">Http请求</param>
        /// <returns></returns>
        private static HttpWebResponse GetResponse(HttpWebRequest request)
        {
            request.Timeout = 30000;//设置30秒后请求超时
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response == null)
            {
                throw new Exception("未获取到服务器响应！");
            }
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new HttpListenerException(400, "请求失败");
                case HttpStatusCode.Forbidden:
                    throw new HttpListenerException(403, "指示服务器拒绝无法完成请求");
                case HttpStatusCode.NotFound:
                    throw new HttpListenerException(404, "请求资源不存在");
                case HttpStatusCode.Gone:
                    throw new HttpListenerException(410, "请求的资源不再可用");
                case HttpStatusCode.InternalServerError:
                    throw new HttpListenerException(500, "服务器上发生一般性错误");
                case HttpStatusCode.NotImplemented:
                    throw new HttpListenerException(501, "服务器不支持所请求的功能");
                case HttpStatusCode.BadGateway:
                    throw new HttpListenerException(502, "中间代理服务器从另一个代理或原始服务器接收到错误响应");
                case HttpStatusCode.GatewayTimeout:
                    throw new HttpListenerException(504, "请求超时");
                case HttpStatusCode.HttpVersionNotSupported:
                    throw new HttpListenerException(505, "服务器不支持请求的 HTTP 版本");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpListenerException(400, "请求失败");
            }
            return response;
        }
        #endregion
    }

    /// <summary>
    /// 异步操作方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class HttpClientAsyncMethodModel<T> where T : class
    {
        delegate T AsyncMethodCaller(String requestUri, RequestParamater requestParamater);
        AsyncMethodCaller caller;
        Action<T> _callback;

        /// <summary>
        /// 异步开始方法
        /// </summary>
        /// <param name="asyncMehotd">异步执行方法</param>
        /// <param name="callback">回调函数</param>
        public HttpClientAsyncMethodModel(Func<String, RequestParamater, T> asyncMehotd,Action<T> callback)
        {
            caller = new AsyncMethodCaller(asyncMehotd);
            _callback = callback;
        }

        /// <summary>
        /// 异步开始方法
        /// </summary>
        /// <param name="requestUri">请求地址</param>
        /// <param name="requestParamater">请求参数</param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public IAsyncResult BeginMethod(String requestUri, RequestParamater requestParamater, AsyncCallback arg1, object arg2 )
        {
            return caller.BeginInvoke(requestUri, requestParamater, arg1, arg2);
        }

        /// <summary>
        /// 异步结束方法
        /// </summary>
        /// <param name="result">异步状态</param>
        /// <returns></returns>
        public T EndMethod(IAsyncResult result)
        {
            //线程阻塞一次
            result.AsyncWaitHandle.WaitOne();
            T returnValue = caller.EndInvoke(result);
            //关闭线程阻塞语柄
            result.AsyncWaitHandle.Close();
            returnValue = returnValue == null ? default(T) : returnValue;

            if (_callback != null)
            {
                _callback.Invoke(returnValue);
            }
            return returnValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XamarinForm.Extensions;

namespace XamarinForm.WebApiService
{
    public class WebApiClient
    {

        private System.Net.Http.HttpClient httpClient;
        String urlTemp = "http://{0}/api";

        /// <summary>
        /// 初始化WebAPI客户端
        /// </summary>
        public WebApiClient()
        {
            httpClient = new System.Net.Http.HttpClient(new System.Net.Http.HttpClientHandler { UseCookies = true });
            httpClient.DefaultRequestHeaders.Add("api_version", "1.1");
            //设置超时时间为10秒
            httpClient.Timeout = new TimeSpan(10 * 1000 * 10000); //单位为100纳秒
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="Method">方法</param>
        /// <param name="requestParamater">请求参数</param>
        public async Task<HttpContent> PostAsync(String Method, RequestParamater requestParamater)
        {
            try
            {
                String jsonParam = requestParamater.ToJson();

                RequestAddSignature(httpClient.DefaultRequestHeaders, jsonParam);
                jsonParam = jsonParam.EncryptDES(ClientConfig.APPSECRET.Substring(0, 8));
                //jsonParam = HttpUtility.UrlEncode(jsonParam); Body 中的数据无需URL编码
                var stringContent = new StringContent(jsonParam, Encoding.UTF8, "application/json");
                var requestUri = Path.Combine(String.Format(urlTemp, ClientConfig.ServiceAddress), Method);

                HttpResponseMessage httpResponse = await httpClient.PostAsync(requestUri, stringContent);
                VerifyResponse(httpResponse);
                RefreshCookie(httpResponse);

                return httpResponse.Content;
            }
            catch (Exception ex)
            {
                throw new Exception("Post请求异常，请与管理员联系！", ex.InnerException);
            }
        }
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="Method">方法</param>
        /// <param name="requestParamater">请求参数</param>
        /// <returns></returns>
        public async Task<HttpContent> GetAsync(String Method, RequestParamater requestParamater)
        {
            String urlParam = requestParamater.ToParamater();

            RequestAddSignature(httpClient.DefaultRequestHeaders, urlParam);
            urlParam = urlParam.EncryptDES(ClientConfig.APPSECRET.Substring(0, 8));
            urlParam = urlParam.UrlEncode();

            var requestUri = Path.Combine(String.Format(urlTemp, ClientConfig.ServiceAddress), Method);
            requestUri = String.Format("{0}?{1}", requestUri, urlParam);

            HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);
            VerifyResponse(httpResponse);
            RefreshCookie(httpResponse);

            return httpResponse.Content;
        }

        /// <summary>
        /// Put 请求
        /// </summary>
        /// <param name="Method">方法</param>
        /// <param name="urlParamater">URL参数</param>
        /// <param name="bodyParamater">body参数</param>
        /// <returns></returns>
        public async Task<HttpContent> PutAsync(String Method, RequestParamater urlParamater, RequestParamater bodyParamater)
        {
            String urlParam = urlParamater.ToParamater();

            RequestAddSignature(httpClient.DefaultRequestHeaders, urlParam);
            urlParam = urlParam.EncryptDES(ClientConfig.APPSECRET.Substring(0, 8));
            urlParam = urlParam.UrlEncode();

            String jsonParam = bodyParamater.ToJson();

            String signatureParam = String.Format("{0}{1}", urlParam, jsonParam);

            RequestAddSignature(httpClient.DefaultRequestHeaders, signatureParam);
            jsonParam = jsonParam.EncryptDES(ClientConfig.APPSECRET.Substring(0, 8));
            //jsonParam = HttpUtility.UrlEncode(jsonParam);  Body 中的数据无需URL编码
            var stringContent = new StringContent(jsonParam, Encoding.UTF8, "application/json");

            var requestUri = Path.Combine(String.Format(urlTemp, ClientConfig.ServiceAddress), Method);
            requestUri = String.Format("{0}?{1}", requestUri, urlParam);

            HttpResponseMessage httpResponse = await httpClient.PutAsync(requestUri, stringContent);
            VerifyResponse(httpResponse);
            RefreshCookie(httpResponse);

            return httpResponse.Content;
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="Method">方法</param>
        /// <param name="urlParamater">参数</param>
        /// <returns></returns>
        public async Task<HttpContent> DeleteAsync(String Method, RequestParamater urlParamater)
        {
            String urlParam = urlParamater.ToParamater();

            RequestAddSignature(httpClient.DefaultRequestHeaders, urlParam);
            urlParam = urlParam.EncryptDES(ClientConfig.APPSECRET.Substring(0, 8));
            urlParam = urlParam.UrlEncode();

            var requestUri = Path.Combine(String.Format(urlTemp, ClientConfig.ServiceAddress), Method);
            requestUri = String.Format("{0}?{1}", requestUri, urlParam);

            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(requestUri);
            VerifyResponse(httpResponse);
            RefreshCookie(httpResponse);

            return httpResponse.Content;

        }

        /// <summary>
        /// 验证响应
        /// </summary>
        /// <param name="response">Http响应</param>
        void VerifyResponse(HttpResponseMessage response)
        {
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
        }

        /// <summary>
        /// 刷新Cookie
        /// </summary>
        /// <param name="httpResponse">http响应</param>
        void RefreshCookie(HttpResponseMessage httpResponse)
        {
            if (httpResponse != null)
            {
                if (httpResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    httpClient.DefaultRequestHeaders.Remove("Set-Cookie");
                    httpClient.DefaultRequestHeaders.Add("Set-Cookie", httpResponse.Headers.FirstOrDefault(p => p.Key.Equals("Set-Cookie")).Value);
                }
            }
        }

        /// <summary>
        /// 给请求添加签名
        /// </summary>
        /// <param name="header"></param>
        /// <param name="param"></param>
        static void RequestAddSignature(HttpRequestHeaders header, String param)
        {
            String timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");

            string Signature = GetSignature(ClientConfig.APPKEY, ClientConfig.APPSECRET, timestamp, param);
            header.Remove("app_key");
            header.Remove("timestamp");
            header.Remove("sign");
            header.Add("app_key", ClientConfig.APPKEY); ;
            header.Add("timestamp", timestamp);
            header.Add("sign", Signature);
        }

        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="secret">签名字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="appparam">请求参数</param>
        /// <returns></returns>
        static String GetSignature(String appkey, String secret, String timestamp, String appparam)
        {
            var hash = System.Security.Cryptography.MD5.Create();

            Dictionary<String, String> dic = new Dictionary<string, string>
            {
                { "param_json", appparam },
                { "app_key", appkey },
                { "timestamp", timestamp }
            };

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
    public class ClientConfig
    {
        static String _serviceAddress="192.168.1.108:8083";
        static String _APPKEY = "904EG960DV56ISV5FI0K51X960UW5ZJT", _APPSECRET = "G6TL12YIL1VX6A1UM526JM15YJB1W26B";
        //static String _APPKEY = String.Empty, _APPSECRET = String.Empty;

        /// <summary>
        /// 获取服务地址
        /// </summary>
        public static String ServiceAddress
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_serviceAddress))
                    throw new Exception("WebApi客户端未设置服务地址！请与管理员联系！");
                return _serviceAddress;
            }
        }

        public static String APPKEY
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_APPKEY))
                    throw new Exception("WebApi客户端未设置APPKEY！请与管理员联系！");
                return _APPKEY;
            }
        }

        public static String APPSECRET
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_APPSECRET))
                    throw new Exception("WebApi客户端未设置APPSECRET！请与管理员联系！");
                return _APPSECRET;
            }
        }

        /// <summary>
        /// 设置服务地址
        /// </summary>
        /// <param name="address"></param>
        public static void SetServiceAddress(String address)
        {
            _serviceAddress = address;
        }

    }
}

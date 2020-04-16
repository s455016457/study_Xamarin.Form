using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace XamarinForm.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 提取Number为Decimal
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static IEnumerable<decimal> ExtractDecimalDesc(this String value)
        {
            string pat = @"(\d*,)*\d+\.*\d*";
            Regex r = new Regex(pat);
            MatchCollection m = r.Matches(value);
            decimal _v = 0;
            for (int i = m.Count - 1; i > 0; i--)
            {
                String _value = m[i].Value.ToString();
                _value = _value.Replace(",", "");

                if (decimal.TryParse(_value, out _v))
                {
                    yield return _v;
                }
            }
        }
        /// <summary>
        /// 提取Number为Int
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static IEnumerable<int> ExtractIntDesc(this String value)
        {
            string pat = @"(\d*,)*\d+";
            Regex r = new Regex(pat);
            MatchCollection m = r.Matches(value);
            int _v = 0;
            for (int i = m.Count - 1; i > 0; i--)
            {
                String _value = m[i].Value.ToString();
                _value = _value.Replace(",", "");

                if (int.TryParse(_value, out _v))
                {
                    yield return _v;
                }
            }
        }


        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        internal static string Key = "*@&$(@#H";

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(this string encryptString, string encryptKey)
        {
            try
            {
                DESCryptoServiceProvider DESalg = new DESCryptoServiceProvider();
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        //// <summary>
        /// DES加密字符串，包含签名字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <param name="secret">签名字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(this string encryptString, string encryptKey, String secret)
        {
            return string.Format("{0}{1}{2}", secret, encryptString, secret).EncryptDES(encryptKey);
        }
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(this string decryptString, string key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        //// <summary>
        /// DES解密字符串，包含签名字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <param name="secret">签名字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(this string decryptString, string key, String secret)
        {
            string value = decryptString.DecryptDES(key);

            if (!value.StartsWith(secret) || !value.EndsWith(secret))
                throw new Exception("解码失败！");

            //去掉首未的签名并返回
            return value.Substring(secret.Length - 1, value.Length - secret.Length * 2); ;
        }

        /// <summary>
        /// Base64 数字编码
        /// </summary>
        /// <param name="encryptString">待编码字符串</param>
        /// <returns></returns>
        public static string EncryptBase64(this string encryptString)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptString));
        }

        /// <summary>
        /// Base64 数字编码，包含签名字符串
        /// </summary>
        /// <param name="encryptString">待编码字符串</param>
        /// <param name="secret">签名字符串</param>
        /// <returns></returns>
        public static string EncryptBase64(this string encryptString, String secret)
        {
            encryptString = String.Format("{0}{1}{2}", secret, encryptString, secret);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptString));
        }

        /// <summary>
        /// Base64 数字解码
        /// </summary>
        /// <param name="encryptString">待解码字符串</param>
        /// <returns></returns>
        public static string DecryptBase64(this string encryptString)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encryptString));
        }

        /// <summary>
        /// Base64 数字解码，包含签名字符串
        /// </summary>
        /// <param name="encryptString">待解码字符串</param>
        /// <param name="secret">签名字符串</param>
        /// <returns></returns>
        public static string DecryptBase64(this string encryptString, String secret)
        {
            string value = encryptString.DecryptBase64();

            if (!value.StartsWith(secret) || !value.EndsWith(secret))
                throw new Exception("解码失败！");

            //去掉首未的签名并返回
            return value.Substring(secret.Length, value.Length - secret.Length * 2);
        }

        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="value">要测试的字符串</param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this String value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="value">要测试的字符串</param>
        /// <returns></returns>
        public static Boolean IsNullOrWhiteSpace(this String value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 获取匹配的值
        /// </summary>
        /// <param name="value">待匹配的字符串</param>
        /// <param name="reg">匹配格式</param>
        /// <returns></returns>
        public static String MatchValue(this String value, string reg)
        {
            Regex regex = new Regex(reg);
            return regex.Match(value).Value;
        }
        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="Value">待解码字符串</param>
        /// <returns>解码的字符串</returns>
        public static String DecodeUrl(this String Value)
        {
            return System.Web.HttpUtility.UrlDecode(Value);
        }
        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="Value">待编码字符串</param>
        /// <returns>编码后的字符串</returns>
        public static String UrlEncode(this String Value)
        {
            return System.Web.HttpUtility.UrlEncode(Value);
        }
    }
}

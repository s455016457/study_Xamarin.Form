using System;
using System.Collections.Generic;
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
    }
}

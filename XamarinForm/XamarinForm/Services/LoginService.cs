using System;
using System.Collections.Generic;
using System.Text;
using XamarinForm.Models;

namespace XamarinForm.Services
{
    public class LoginService
    {
        /// <summary>
        /// 票据有效时间，分钟
        /// </summary>
        const int BillLegalTime = 30;

        /// <summary>
        /// 验证票据
        /// </summary>
        /// <param name="bill">票据</param>
        /// <returns></returns>
        public static Boolean VerificationBill(LoginBill bill)
        {
            if (bill == null) return false;
            if (bill.BillTime.AddMinutes(BillLegalTime) < DateTime.Now)
            {
                return false;
            }
            if (VeriFicationLoginUsername(bill.UserName, bill.Password))
            {
                bill.UpdateBillTime();
                return true;
            }
            return false;
        }

        private static Boolean VeriFicationLoginUsername(String UserName, String Passwrod)
        {
            return true;
        }
    }
}

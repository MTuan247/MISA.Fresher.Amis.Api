using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Utils
{
    public class Validator
    {
        //Pattern Email
        public const string EMAIL_PATTERN = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        //Pattern Phone number
        public const string PHONE_NUMBER_PATTERN = @"^[0-9\-\+]{9,15}$";

        /// <summary>
        /// Kiểm tra email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (05/08/2021)
        /// ModifiedBy: NMTuan (05/08/2021)
        public static bool CheckEmail(string email)
        {
            Regex regex = new Regex(EMAIL_PATTERN);
            return regex.IsMatch(email);
        }

        /// <summary>
        /// Kiểm tra số điện thoại
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (05/08/2021)
        /// ModifiedBy: NMTuan (05/08/2021)
        public static bool CheckPhoneNumber(string phone)
        {
            Regex regex = new Regex(PHONE_NUMBER_PATTERN);
            return regex.IsMatch(phone);
        }

    }
}

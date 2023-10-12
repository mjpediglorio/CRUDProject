using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helper
{
    public static class Extensions
    {
        public static int StringToInt(this string mystring)
        {
            int ret = 0;

            if (int.TryParse(mystring, out ret))
                return ret;
            else
                return 0;
        }

        public static int ObjectToInt(this object mystring)
        {
            int ret = 0;

            if (int.TryParse(mystring.ToString(), out ret))
                return ret;
            else
                return 0;
        }

        public static double ObjectToDouble(this object mystring)
        {
            double ret = 0d;

            if (double.TryParse(mystring.ToString(), out ret))
                return ret;
            else
                return 0d;
        }

        public static decimal ObjectToDecimal(this object mystring)
        {
            decimal ret = 0;

            if (decimal.TryParse(mystring.ToString(), out ret))
                return ret;
            else
                return 0;
        }

        public static DateTime ObjectToDateTime(this object mystring)
        {
            DateTime ret = new DateTime();

            if (DateTime.TryParse(mystring.ToString(), out ret))
                return ret;
            else
                return new DateTime();
        }

        public static DateTime UnixToDateTime(this int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        public static int DateTimeToUnixTime(this DateTime date)
        {
            Int32 unixTimestamp = (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Trackify
{
    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public Date (string date)
        {
            //DDMMYYY
            this.Day = Convert.ToInt32(date.Substring(0, 2));
            this.Month = Convert.ToInt32(date.Substring(2, 2));
            this.Year = Convert.ToInt32(date.Substring(4, 4));
        }
        public static string FromDate(Date a)
        {

            string date = "";
            if(a != null && a.Day != null && a.Month != null && a.Year != null)
            {
                date = date + a.Day.ToString("00");
                date = date + a.Month.ToString("00");
                date = date + a.Year.ToString("0000");
                return date;
            }
            else
            {
                return "01011900";
            }
        }
    }
}
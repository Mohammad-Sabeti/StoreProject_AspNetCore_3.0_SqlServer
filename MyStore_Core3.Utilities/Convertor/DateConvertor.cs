using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MyStore_Core3.Utilities.Convertor
{
  public  static  class DateConvertor
    {
        public static string ToShamsi(this DateTime vTime)
        {
            PersianCalendar pcCalendar = new PersianCalendar();
            return pcCalendar.GetYear(vTime) + "/" + pcCalendar.GetMonth(vTime).ToString("00") + "/" +
                   pcCalendar.GetDayOfMonth(vTime).ToString("00");
        }
    }
}

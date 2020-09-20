using System;
using System.Collections.Generic;
using System.Text;

namespace MyStore_Core3.Utilities.Convertor
{
   public static class PriceConvertor
    {
        public static string Tooman(this decimal value)
        {
            return value.ToString("#,0 تومان");
        }
    }
}

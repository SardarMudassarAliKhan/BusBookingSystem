using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    public static class Formator
    {
        public static string Format(this decimal value)
        {
            return String.Format("{0:0.00}", value);
        }
        public static string Format(this decimal? value)
        {
            if (value is null) return "0.00";
            return String.Format("{0:0.00}", value);
        }
    }
}

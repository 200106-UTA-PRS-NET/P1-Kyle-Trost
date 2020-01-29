using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PizzaBoxWebsite
{
    public static class Globals
    {
        public const decimal SALES_TAX = 0.08M;
        public const decimal MAX_ORDER_COST = 250.00M;
        public const int MAX_PIZZAS = 100;
        public const int ORDER_INTERVAL_DAYS = 1;

        static NumberFormatInfo nfi = new NumberFormatInfo();

        public static NumberFormatInfo GetNfi()
        {
            nfi.NumberDecimalDigits = 2;
            return nfi;
        }
    }
}

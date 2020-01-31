using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBoxWebsite.Models;

namespace PizzaBoxWebsite
{
    public static class Globals
    {
        public const decimal SALES_TAX = 0.08M;
        public const decimal MAX_ORDER_COST = 250.00M;
        public const int MAX_PIZZAS = 100;
        public const int ORDER_INTERVAL_DAYS = 1;

        //public static IPizzasSoldRepository<PizzaSold> _pizzaRepo = Dependencies.CreatePizzasSoldRepository();
        public static List<PizzaSold/*ViewModel*/> pizzaList = new List<PizzaSold/*ViewModel*/>();

        public static ISizeRepository<Size> sizeRepo = Dependencies.CreateSizeRepository();
        public static ICrustTypeRepository<CrustType> crustRepo = Dependencies.CreateCrustTypeRepository();
        public static IStoreRepository<Store> storeRepo = Dependencies.CreateStoreRepository();

        static NumberFormatInfo nfi = new NumberFormatInfo();

        public static User CurrentUser { get; set; }

        public static NumberFormatInfo GetNfi()
        {
            nfi.NumberDecimalDigits = 2;
            return nfi;
        }
    }
}

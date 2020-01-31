using System;
using System.Collections.Generic;
using System.Text;

using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain
{
    public class Mapper
    {
        #region crust type mapping
        public static CrustType MapCrustType(Models.CrustTypes crust)
        {
            return new CrustType
            {
                CrustId = crust.CrustId,
                CrustName = crust.CrustName,
                CrustCost = crust.CrustCost
            };
        }

        public static Models.CrustTypes MapCrustType(CrustType crust)
        {
            return new Models.CrustTypes
            {
                CrustId = crust.CrustId,
                CrustName = crust.CrustName,
                CrustCost = crust.CrustCost
            };
        }
        #endregion

        #region order mapping
        public static Order MapOrder(Models.Orders order)
        {
            return new Order
            {
                OrderId = order.OrderId,
                User = order.User,
                UserId = order.UserId,
                PizzasSold = order.PizzasSold,
                Store = order.Store,
                StoreId = order.StoreId,
                TotalCost = order.TotalCost,
                OrderTimestamp = order.OrderTimestamp
            };
        }

        public static Models.Orders MapOrder(Order order)
        {
            return new Models.Orders
            {
                OrderId = order.OrderId,
                User = order.User,
                UserId = order.UserId,
                PizzasSold = order.PizzasSold,
                Store = order.Store,
                StoreId = order.StoreId,
                TotalCost = order.TotalCost,
                OrderTimestamp = order.OrderTimestamp
            };
        }
        #endregion

        #region pizzas sold mapping
        public static PizzaSold MapPizzaSold(Models.PizzasSold pizza)
        {
            return new PizzaSold
            {
                OrderId = pizza.OrderId,
                PizzaName = pizza.PizzaName,
                PizzaSize = pizza.PizzaSize,
                PizzaCrust = pizza.PizzaCrust,
                TotalCost = pizza.TotalCost,
                Order = pizza.Order,
                PizzaCrustNavigation = pizza.PizzaCrustNavigation,
                PizzaSizeNavigation = pizza.PizzaSizeNavigation
            };
        }

        public static Models.PizzasSold MapPizzaSold(PizzaSold pizza)
        {
            return new Models.PizzasSold
            {
                OrderId = pizza.OrderId,
                PizzaName = pizza.PizzaName,
                PizzaSize = pizza.PizzaSize,
                PizzaCrust = pizza.PizzaCrust,
                TotalCost = pizza.TotalCost,
                Order = pizza.Order,
                PizzaCrustNavigation = pizza.PizzaCrustNavigation,
                PizzaSizeNavigation = pizza.PizzaSizeNavigation
            };
        }
        #endregion

        #region preset pizza mapping
        public static PresetPizza MapPresetPizza(Models.PresetPizzas pizza)
        {
            return new PresetPizza
            {
                PresetId = pizza.PresetId,
                PresetName = pizza.PresetName
            };
        }

        public static Models.PresetPizzas MapPresetPizza(PresetPizza pizza)
        {
            return new Models.PresetPizzas
            {
                PresetId = pizza.PresetId,
                PresetName = pizza.PresetName
            };
        }
        #endregion

        #region size mapping
        public static Size MapSize(Models.Sizes size)
        {
            return new Size
            {
                SizeId = size.SizeId,
                SizeName = size.SizeName,
                SizeCost = size.SizeCost
            };
        }

        public static Models.Sizes MapSize(Size size)
        {
            return new Models.Sizes
            {
                SizeId = size.SizeId,
                SizeName = size.SizeName,
                SizeCost = size.SizeCost
            };
        }
        #endregion

        #region store mapping
        public static Store MapStore(Models.Stores store)
        {
            return new Store
            {
                StoreId = store.StoreId,
                StoreLocation = store.StoreLocation,
                Orders = store.Orders
            };
        }

        public static Models.Stores MapStore(Store store)
        {
            return new Models.Stores
            {
                StoreId = store.StoreId,
                StoreLocation = store.StoreLocation,
                Orders = store.Orders
            };
        }
        #endregion

        #region user mapping
        public static User MapUser(Models.Users user)
        {
            return new User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserPass = user.UserPass,
                StoreId = user.StoreId
            };
        }

        public static Models.Users MapUser(User user)
        {
            return new Models.Users
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserPass = user.UserPass,
                StoreId = user.StoreId
            };
        }
        #endregion
    }
}

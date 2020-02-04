using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaBox.Domain;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Storing.Repositories
{
    public class OrderRepository : IOrderRepository<Order>
    {
        PizzaBoxDbContext db;

        public OrderRepository()
        {
            db = new PizzaBoxDbContext();
        }

        public OrderRepository(PizzaBoxDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddOrder(Order order)
        {
            if (db.Orders.Any(o => o.OrderId == order.OrderId))
            {
                Console.WriteLine($"Order with ID {order.OrderId} already exists and cannot be added");
                return;
            }
            else
            {
                Console.WriteLine($"Adding order...");
                db.Orders.Add(Mapper.MapOrder(order));
                db.SaveChanges();
                Console.WriteLine($"Order added successfully");
            }
        }

        public IEnumerable<Order> GetOrders(int? storeId, int userId/*, bool sortByTimestamp*/)
        {
            IQueryable<Order> query;

            if(storeId != -1)
            {
                query = from o in db.Orders
                        where o.StoreId == storeId
                        orderby o.OrderTimestamp descending
                        select Mapper.MapOrder(o);
            }
            else if(userId != -1)
            {
                query = from o in db.Orders
                        where o.UserId == userId
                        orderby o.OrderTimestamp descending
                        select Mapper.MapOrder(o);
            }
            else
            {
                query = from o in db.Orders
                        orderby o.OrderTimestamp descending
                        select Mapper.MapOrder(o);
            }

            return query;
        }

        public void ModifyOrder(Order order)
        {
            if (db.Orders.Any(o => o.OrderId == o.OrderId))
            {
                var orderTemp = db.Orders.FirstOrDefault(o => o.OrderId == o.OrderId);
                orderTemp.User = order.User;
                orderTemp.UserId = order.UserId;
                orderTemp.PizzasSold = order.PizzasSold;
                orderTemp.Store = order.Store;
                orderTemp.StoreId = order.StoreId;
                orderTemp.TotalCost = order.TotalCost;
                db.Orders.Update(orderTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Order with ID {order.OrderId} does not exist");
                return;
            }
        }

        public void RemoveOrder(int id)
        {
            var orderTemp = db.Orders.FirstOrDefault(o => o.OrderId == id);
            if (orderTemp.OrderId == id)
            {
                Console.WriteLine($"Removing order with ID {id}...");
                db.Remove(orderTemp);
                db.SaveChanges();
                Console.WriteLine($"Order with ID {id} removed successfully");
            }
            else
            {
                Console.WriteLine($"Order with ID {id} does not exist");
                return;
            }
        }
    }
}

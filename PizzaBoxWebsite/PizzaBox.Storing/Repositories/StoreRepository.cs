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
    public class StoreRepository : IStoreRepository<Store>
    {
        PizzaBoxDbContext db;

        public StoreRepository()
        {
            db = new PizzaBoxDbContext();
        }

        public StoreRepository(PizzaBoxDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddStore(Store store)
        {
            if (db.Stores.Any(s => s.StoreLocation == store.StoreLocation) || store.StoreLocation == null)
            {
                Console.WriteLine($"Store {store.StoreLocation} already exists and cannot be added");
                return;
            }
            else
            {
                Console.WriteLine($"Adding store {store.StoreLocation}...");
                db.Stores.Add(Mapper.MapStore(store));
                db.SaveChanges();
                Console.WriteLine($"Store {store.StoreLocation} added successfully");
            }
        }

        public IEnumerable<Store> GetStores(int? storeId)
        {
            IEnumerable<Store> query;

            if (storeId != -1)
            {
                query = from s in db.Stores
                        where s.StoreId == storeId
                        select Mapper.MapStore(s);
            }
            else
            {
                query = from s in db.Stores
                        select Mapper.MapStore(s);
            }

            return query;
        }

        public void ModifyStore(Store store)
        {
            if (db.Stores.Any(s => s.StoreId == store.StoreId))
            {
                var storeTemp = db.Stores.FirstOrDefault(s => s.StoreId == store.StoreId);
                storeTemp.StoreLocation = store.StoreLocation;
                db.Stores.Update(storeTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Store with ID {store.StoreId} does not exist");
                return;
            }
        }

        public void RemoveStore(int id)
        {
            var storeTemp = db.Stores.FirstOrDefault(s => s.StoreId == id);
            if (storeTemp.StoreId == id)
            {
                db.Remove(storeTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"User with ID {id} does not exist");
                return;
            }
        }
    }
}

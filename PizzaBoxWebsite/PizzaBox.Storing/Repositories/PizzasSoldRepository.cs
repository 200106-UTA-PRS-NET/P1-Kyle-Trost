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
    public class PizzasSoldRepository : IPizzasSoldRepository<PizzaSold>
    {
        PizzaBoxDbContext db;

        public PizzasSoldRepository()
        {
            db = new PizzaBoxDbContext();
        }

        public PizzasSoldRepository(PizzaBoxDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddPizzaSold(PizzaSold pizza)
        {
            if (db.PizzasSold.Any(p => p.PizzaId == pizza.PizzaId))
            {
                Console.WriteLine($"Preset pizza {pizza.PizzaName} already exists and cannot be added");
                return;
            }
            else
            {
                Console.WriteLine($"Adding pizza {pizza.PizzaName}...");
                db.PizzasSold.Add(Mapper.MapPizzaSold(pizza));
                db.SaveChanges();
                Console.WriteLine($"Pizza {pizza.PizzaName} added successfully");
            }

            //Console.WriteLine($"Adding pizza {pizza.PizzaName} to order...");
            //db.PizzasSold.Add(Mapper.MapPizzaSold(pizza));
            //db.SaveChanges();
            //Console.WriteLine($"Pizza {pizza.PizzaName} added successfully");
        }

        public IEnumerable<PizzaSold> GetPizzasSold(int storeId, int orderId)
        {
            IQueryable<PizzaSold> query;

            if (orderId != -1)
            {
                query = from p in db.PizzasSold
                        where p.OrderId == orderId
                        select Mapper.MapPizzaSold(p);
            }
            else
            {
                query = from p in db.PizzasSold
                        select Mapper.MapPizzaSold(p);
            }

            return query;
        }

        public void ModifyPizzaSold(PizzaSold pizza)
        {
            //if (db.PizzasSold.Any(p => p.PresetId == pizza.PresetId))
            //{
            //    var presetTemp = db.PresetPizzas.FirstOrDefault(p => p.PresetId == pizza.PresetId);
            //    presetTemp.PresetName = presetTemp.PresetName;
            //    db.PresetPizzas.Update(presetTemp);
            //    db.SaveChanges();
            //}
            //else
            //{
            //    Console.WriteLine($"Preset pizza with ID {pizza.PresetId} does not exist");
            //    return;
            //}
        }

        public void RemovePizzaSold(int id)
        {
            //throw new NotImplementedException();
        }
    }
}

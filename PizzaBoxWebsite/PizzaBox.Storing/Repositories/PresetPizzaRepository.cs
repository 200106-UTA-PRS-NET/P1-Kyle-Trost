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
    public class PresetPizzaRepository : IPresetPizzaRepository<PresetPizza>
    {
        PizzaBoxDbContext db;

        public PresetPizzaRepository()
        {
            db = new PizzaBoxDbContext();
        }

        public PresetPizzaRepository(PizzaBoxDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddPresetPizza(PresetPizza pizza)
        {
            if (db.PresetPizzas.Any(p => p.PresetName == pizza.PresetName) || pizza.PresetName == null)
            {
                Console.WriteLine($"Preset pizza {pizza.PresetName} already exists and cannot be added");
                return;
            }
            else
            {
                Console.WriteLine($"Adding preset pizza {pizza.PresetName}...");
                db.PresetPizzas.Add(Mapper.MapPresetPizza(pizza));
                db.SaveChanges();
                Console.WriteLine($"User {pizza.PresetName} added successfully");
            }
        }

        public IEnumerable<PresetPizza> GetPresetPizzas(int choice)
        {
            var query = (choice != -1)
                        ? from p in db.PresetPizzas
                          where p.PresetId == choice
                          select Mapper.MapPresetPizza(p)
                        : from p in db.PresetPizzas
                          select Mapper.MapPresetPizza(p);

            return query;
        }

        public void ModifyPresetPizza(PresetPizza pizza)
        {
            if (db.PresetPizzas.Any(p => p.PresetId == pizza.PresetId))
            {
                var presetTemp = db.PresetPizzas.FirstOrDefault(p => p.PresetId == pizza.PresetId);
                presetTemp.PresetName = presetTemp.PresetName;
                db.PresetPizzas.Update(presetTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Preset pizza with ID {pizza.PresetId} does not exist");
                return;
            }
        }

        public void RemovePresetPizza(int id)
        {
            var presetTemp = db.PresetPizzas.FirstOrDefault(p => p.PresetId == id);
            if (presetTemp.PresetId == id)
            {
                db.Remove(presetTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Preset pizza with ID {id} does not exist");
                return;
            }
        }
    }
}

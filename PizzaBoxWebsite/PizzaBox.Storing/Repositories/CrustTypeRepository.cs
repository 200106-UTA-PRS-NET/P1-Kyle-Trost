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
    public class CrustTypeRepository : ICrustTypeRepository<CrustType>
    {
        PizzaBoxDbContext db;

        public CrustTypeRepository()
        {
            db = new PizzaBoxDbContext();
        }

        public CrustTypeRepository(PizzaBoxDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddCrust(CrustType crust)
        {
            if (db.CrustTypes.Any(c => c.CrustName == crust.CrustName))
            {
                Console.WriteLine($"Crust type {crust.CrustName} already exists and cannot be added");
                return;
            }
            else
            {
                Console.WriteLine($"Adding crust type {crust.CrustName}...");
                db.CrustTypes.Add(Mapper.MapCrustType(crust));
                db.SaveChanges();
                Console.WriteLine($"Crust type {crust.CrustName} added successfully");
            }
        }

        public IEnumerable<CrustType> GetCrustTypes(int? crustId)
        {
            IEnumerable<CrustType> query;

            if (crustId != -1)
            {
                query = from c in db.CrustTypes
                        where c.CrustId == crustId
                        select Mapper.MapCrustType(c);
            }
            else
            {
                query = from c in db.CrustTypes
                        select Mapper.MapCrustType(c);
            }

            return query;
        }

        public void ModifyCrust(CrustType crust)
        {
            if (db.CrustTypes.Any(c => c.CrustId == c.CrustId))
            {
                var crustTemp = db.CrustTypes.FirstOrDefault(c => c.CrustId == c.CrustId);
                crustTemp.CrustId = crust.CrustId;
                crustTemp.CrustName = crust.CrustName;
                crustTemp.CrustCost = crust.CrustCost;
                db.CrustTypes.Update(crustTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Crust type with ID {crust.CrustId} does not exist");
                return;
            }
        }

        public void RemoveCrust(int id)
        {
            var crustTemp = db.CrustTypes.FirstOrDefault(c => c.CrustId == id);
            if (crustTemp.CrustId == id)
            {
                Console.WriteLine($"Removing crust type with ID {id}...");
                db.Remove(crustTemp);
                db.SaveChanges();
                Console.WriteLine($"Crust type with ID {id} removed successfully");
            }
            else
            {
                Console.WriteLine($"Crust type with ID {id} does not exist");
                return;
            }
        }
    }
}

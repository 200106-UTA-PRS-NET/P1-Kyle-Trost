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
    public class SizeRepository : ISizeRepository<Size>
    {
        PizzaBoxDbContext db;

        public SizeRepository()
        {
            db = new PizzaBoxDbContext();
        }

        public SizeRepository(PizzaBoxDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddSize(Size size)
        {
            if (db.Sizes.Any(s => s.SizeName == size.SizeName) || size.SizeName == null)
            {
                Console.WriteLine($"Size {size.SizeName} already exists and cannot be added");
                return;
            }
            else
            {
                Console.WriteLine($"Adding size {size.SizeName}...");
                db.Sizes.Add(Mapper.MapSize(size));
                db.SaveChanges();
                Console.WriteLine($"Size {size.SizeName} added successfully");
            }
        }
        public IEnumerable<Size> GetSizes(int? sizeId)
        {
            IQueryable<Size> query;

            if (sizeId != -1)
            {
                 query = from s in db.Sizes
                         where s.SizeId == sizeId
                         select Mapper.MapSize(s);
            }
            else
            {
                query = from s in db.Sizes
                        select Mapper.MapSize(s);
            }

            return query;
        }

        public void ModifySize(Size size)
        {
            if (db.Sizes.Any(s => s.SizeId == size.SizeId))
            {
                var sizeTemp = db.Sizes.FirstOrDefault(s => s.SizeId == size.SizeId);
                sizeTemp.SizeName = size.SizeName;
                sizeTemp.SizeCost = size.SizeCost;
                db.Sizes.Update(sizeTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Size with ID {size.SizeId} does not exist");
                return;
            }
        }

        public void RemoveSize(int id)
        {
            var sizeTemp = db.Sizes.FirstOrDefault(s => s.SizeId == id);
            if (sizeTemp.SizeId == id)
            {
                db.Remove(sizeTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Size with ID {id} does not exist");
                return;
            }
        }
    }
}

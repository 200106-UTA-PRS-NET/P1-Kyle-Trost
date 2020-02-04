using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Testing.Mocks
{
    class MockRepositorySize : ISizeRepository<Size>
    {
        static readonly IEnumerable<Size> sizes = new List<Size>()
        {
            new Size()
            {
                SizeId = 1,
                SizeName = "Gigantic",
                SizeCost = 1.00M
            },
            new Size()
            {
                SizeId = 2,
                SizeName = "Galactic",
                SizeCost = 1.00M
            }
        };

        public void AddSize(Size size)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Size> GetSizes(int? sizeId = -1)
        {
            return (sizeId != -1) ? sizes.Where(s => s.SizeId == sizeId) : sizes;
        }

        public void ModifySize(Size size)
        {
            throw new NotImplementedException();
        }

        public void RemoveSize(int id)
        {
            throw new NotImplementedException();
        }
    }
}

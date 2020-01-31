using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface ISizeRepository<T>
    {
        IEnumerable<T> GetSizes(int? sizeId = -1);
        void AddSize(T size);
        void ModifySize(T size);
        void RemoveSize(int id);
    }
}

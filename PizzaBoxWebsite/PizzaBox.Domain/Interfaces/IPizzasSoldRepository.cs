using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface IPizzasSoldRepository<T>
    {
        IEnumerable<T> GetPizzasSold(int storeId = -1, int orderId = -1);
        void AddPizzaSold(T pizza);
        void ModifyPizzaSold(T pizza);
        void RemovePizzaSold(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface IStoreRepository<T>
    {
        IEnumerable<T> GetStores(int? storeId = -1);
        void AddStore(T store);
        void ModifyStore(T store);
        void RemoveStore(int id);
    }
}

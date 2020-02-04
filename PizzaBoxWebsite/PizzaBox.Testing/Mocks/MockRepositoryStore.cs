using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Testing.Mocks
{
    class MockRepositoryStore : IStoreRepository<Store>
    {
        static readonly IEnumerable<Store> stores = new List<Store>()
        {
            new Store()
            {
                StoreId = 1,
                StoreLocation = "Toronto_CN"
            },
            new Store()
            {
                StoreId = 2,
                StoreLocation = "London_ENG"
            }
        };

        public void AddStore(Store store)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Store> GetStores(int? storeId = -1)
        {
            return (storeId != -1) ? stores.Where(s => s.StoreId == storeId) : stores;
        }

        public void ModifyStore(Store store)
        {
            throw new NotImplementedException();
        }

        public void RemoveStore(int id)
        {
            throw new NotImplementedException();
        }
    }
}

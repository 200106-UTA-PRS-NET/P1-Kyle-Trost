using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Testing.Mocks
{
    class MockRepositoryCrustType : ICrustTypeRepository<CrustType>
    {
        static readonly IEnumerable<CrustType> crusts = new List<CrustType>()
        {
            new CrustType()
            {
                CrustId = 1,
                CrustName = "Earth",
                CrustCost = 2.00M
            },
            new CrustType()
            {
                CrustId = 2,
                CrustName = "Upper",
                CrustCost = 5.00M
            }
        };

        public void AddCrust(CrustType crust)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CrustType> GetCrustTypes(int? crustId = -1)
        {
            return (crustId != -1) ? crusts.Where(c => c.CrustId == crustId) : crusts;
        }

        public void ModifyCrust(CrustType crust)
        {
            throw new NotImplementedException();
        }

        public void RemoveCrust(int id)
        {
            throw new NotImplementedException();
        }
    }
}

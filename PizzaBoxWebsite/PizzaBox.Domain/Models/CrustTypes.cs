using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class CrustTypes
    {
        public CrustTypes()
        {
            PizzasSold = new HashSet<PizzasSold>();
        }

        public int CrustId { get; set; }
        public string CrustName { get; set; }
        public decimal CrustCost { get; set; }

        public virtual ICollection<PizzasSold> PizzasSold { get; set; }
    }
}

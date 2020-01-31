using System;
using System.Collections.Generic;
using System.Text;

using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public class CrustType
    {
        public CrustType()
        {
            PizzasSold = new HashSet<PizzasSold>();
        }

        public int CrustId { get; set; }
        public string CrustName { get; set; }
        public decimal CrustCost { get; set; }

        public virtual ICollection<PizzasSold> PizzasSold { get; set; }
    }
}

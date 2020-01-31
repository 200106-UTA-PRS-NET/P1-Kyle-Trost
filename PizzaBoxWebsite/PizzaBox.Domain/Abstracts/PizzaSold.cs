using System;
using System.Collections.Generic;
using System.Text;

using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public class PizzaSold
    {
        public int? OrderId { get; set; }
        public string PizzaName { get; set; }
        public int? PizzaSize { get; set; }
        public int? PizzaCrust { get; set; }
        public decimal? TotalCost { get; set; }
        public int PizzaId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual CrustTypes PizzaCrustNavigation { get; set; }
        public virtual Sizes PizzaSizeNavigation { get; set; }
    }
}

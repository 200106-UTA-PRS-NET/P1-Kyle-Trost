using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class PizzasSold
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

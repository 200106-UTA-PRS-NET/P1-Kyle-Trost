using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Sizes
    {
        public Sizes()
        {
            PizzasSold = new HashSet<PizzasSold>();
        }

        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public decimal SizeCost { get; set; }

        public virtual ICollection<PizzasSold> PizzasSold { get; set; }
    }
}

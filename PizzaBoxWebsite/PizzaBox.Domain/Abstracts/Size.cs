using System;
using System.Collections.Generic;
using System.Text;

using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public class Size
    {
        public Size()
        {
            PizzasSold = new HashSet<PizzasSold>();
        }

        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public decimal SizeCost { get; set; }

        public virtual ICollection<PizzasSold> PizzasSold { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public class Order
    {
        public Order()
        {
            PizzasSold = new HashSet<PizzasSold>();
        }

        public int OrderId { get; set; }
        public int? StoreId { get; set; }
        public int? UserId { get; set; }
        public decimal? TotalCost { get; set; }
        public DateTime? OrderTimestamp { get; set; }

        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<PizzasSold> PizzasSold { get; set; }
    }
}

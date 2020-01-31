using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Stores
    {
        public Stores()
        {
            Orders = new HashSet<Orders>();
        }

        public int StoreId { get; set; }
        public string StoreLocation { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public class Store
    {
        public Store()
        {
            Orders = new HashSet<Orders>();
        }

        public int StoreId { get; set; }
        public string StoreLocation { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Orders>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public int? StoreId { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}

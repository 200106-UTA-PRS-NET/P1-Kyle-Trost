using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Users
    {
        public Users()
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

using PizzaBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWebsite.Models
{
    public class OrderViewModel
    {
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

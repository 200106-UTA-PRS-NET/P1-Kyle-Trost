using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWebsite.Models
{
    public class StoreViewModel
    {
        [Display(Name = "Store ID")]
        public int StoreId { get; set; }
        [Display(Name = "Store Location")]
        public string StoreLocation { get; set; }
    }
}

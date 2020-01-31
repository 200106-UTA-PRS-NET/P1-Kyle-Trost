using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PizzaBox.Domain.Models;

namespace PizzaBoxWebsite.Models
{
    public class PizzaSoldViewModel
    {
        public int? OrderId { get; set; }
        public string PizzaName { get; set; }
        public int? PizzaSize 
        {
            //get
            //{
            //    return PizzaSize;
            //}
            //set
            //{
            //    PizzaSize = value;
            //    PizzaSizeName = PizzaSize switch
            //    {
            //        1 => "Small",
            //        2 => "Medium",
            //        3 => "Large",
            //        _ => "Size"
            //    };
            //}
            get;set;
        }
        //public string PizzaSizeName { get; set; }
        public int? PizzaCrust 
        {
            //get
            //{
            //    return PizzaCrust;
            //}
            //set
            //{
            //    PizzaCrust = value;
            //    PizzaCrustName = PizzaCrust switch
            //    {
            //        1 => "Normal",
            //        2 => "Thin",
            //        3 => "Stuffed",
            //        _ => "Crust",
            //    };
            //}
            get;set;
         }
        //public string PizzaCrustName { get; set; }
        public decimal? TotalCost 
        { 
            get; 
            set;
        }
        public int PizzaId { get; set; }

        //public virtual Orders Order { get; set; }
        //public virtual CrustTypes PizzaCrustNavigation { get; set; }
        //public virtual Sizes PizzaSizeNavigation { get; set; }
    }
}

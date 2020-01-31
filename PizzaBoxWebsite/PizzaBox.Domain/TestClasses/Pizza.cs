using System;
using System.Runtime.Serialization;

namespace PizzaBox.Domain
{
    #region Enums
    public enum PizzaSize
    {
        Small,
        Medium,
        Large
    }

    public enum PizzaCrust
    {
        Normal,
        Thin,
        Stuffed
    }
    #endregion

    #region Classes
    [DataContract]
    public class Pizza : IItem
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public PizzaSize SizeOfPizza { get; set; }
        [DataMember]
        public PizzaCrust Crust { get; set; }

        public Pizza(PizzaSize size, PizzaCrust crust)
        {
            this.Name = "Pizza";

            this.SizeOfPizza = size;
            this.Crust = crust;

            CalculateCost();
        }

        void CalculateCost()
        {
            Price = 0.00M;

            switch (this.SizeOfPizza)
            {
                case PizzaSize.Small:
                    Price += 7.99M;
                    break;
                case PizzaSize.Medium:
                    this.Price += 11.99M;
                    break;
                case PizzaSize.Large:
                    this.Price += 14.99M;
                    break;
            }

            if (this.Crust == PizzaCrust.Stuffed) Price += 2.00M;
        }
    }
    #endregion
}

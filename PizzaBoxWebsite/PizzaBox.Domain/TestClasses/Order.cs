using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

//namespace PizzaBox.Domain.Models
//{
//    [DataContract]
//    public class Order
//    {
//        //[DataMember]
//        List<Pizza> orderedPizzas;
//        decimal totalCost;

//        [DataMember]
//        public List<Pizza> OrderedPizzas 
//        { 
//            get
//            {
//                return orderedPizzas;
//            }
//            set
//            {

//            }
//        }

//        public Order()
//        {
//            orderedPizzas = new List<Pizza>();
//        }

//        public Order(PizzaSize pizzaSize, PizzaCrust pizzaCrust)
//        {
//            orderedPizzas = new List<Pizza>();

//            AddToOrder(pizzaSize, pizzaCrust);
//        }

//        public List<Pizza> GetOrderedPizzas() { return orderedPizzas; }

//        public void AddToOrder(PizzaSize pizzaSize, PizzaCrust pizzaCrust)
//        {

//            var pizza = new Pizza(pizzaSize, pizzaCrust);

//            if (totalCost + (pizza.Price + (pizza.Price * Globals.SALES_TAX)) <= Globals.MAX_ORDER_COST)
//            {
//                orderedPizzas.Add(pizza);
//                CalculateTotalCost();
//            }
//            else
//            {
//                Console.WriteLine($"You cannot order more than ${Globals.MAX_ORDER_COST.ToString("N", Globals.GetNfi())} " +
//                    $"worth of pizza");
//            }
//        }

//        //public Pizza GetOrderedPizza() { return orderedPizza; }
//        public decimal GetTotalCost() { return totalCost; }
//        private void CalculateTotalCost()
//        {
//            decimal initialCost = 0.00M;

//            foreach(var pizza in orderedPizzas)
//            {
//                initialCost += pizza.Price;
//            }

//            decimal salesTax = initialCost * Globals.SALES_TAX;

//            totalCost = (initialCost + salesTax > 250.00M) ? 250.00M : initialCost + salesTax;
//        }
//    }
//}

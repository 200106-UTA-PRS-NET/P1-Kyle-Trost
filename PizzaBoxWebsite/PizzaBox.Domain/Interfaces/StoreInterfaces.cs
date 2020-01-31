using System.Collections.Generic;

namespace PizzaBox.Domain
{
    public interface IItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public interface IInventory
    {
        public int GetNumberInStorage(string itemName);
        public void RemoveFromStorage(string itemName);
    }
}

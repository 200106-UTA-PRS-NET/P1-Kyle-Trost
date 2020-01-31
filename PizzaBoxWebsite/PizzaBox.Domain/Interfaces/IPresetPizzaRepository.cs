using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface IPresetPizzaRepository<T>
    {
        IEnumerable<T> GetPresetPizzas(int choice = -1);
        void AddPresetPizza(T pizza);
        void ModifyPresetPizza(T pizza);
        void RemovePresetPizza(int id);
    }
}

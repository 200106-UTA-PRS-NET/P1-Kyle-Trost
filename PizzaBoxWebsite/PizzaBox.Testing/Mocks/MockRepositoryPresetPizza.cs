using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Testing.Mocks
{
    class MockRepositoryPresetPizza : IPresetPizzaRepository<PresetPizza>
    {
        static readonly IEnumerable<PresetPizza> presets = new List<PresetPizza>()
        {
            new PresetPizza()
            {
                PresetId = 1,
                PresetName = "Pizza1"
            },
            new PresetPizza()
            {
                PresetId = 2,
                PresetName = "Pizza2"
            }
        };

        public void AddPresetPizza(PresetPizza pizza)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PresetPizza> GetPresetPizzas(int choice = -1)
        {
            return (choice != -1) ? presets.Where(p => p.PresetId == choice) : presets;
        }

        public void ModifyPresetPizza(PresetPizza pizza)
        {
            throw new NotImplementedException();
        }

        public void RemovePresetPizza(int id)
        {
            throw new NotImplementedException();
        }
    }
}

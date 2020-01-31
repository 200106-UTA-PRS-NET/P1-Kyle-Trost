using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;

namespace PizzaBoxWebsite
{
    public class Dependencies
    {
        public static PizzaBoxDbContext GetDbContext()
        {
            var configurBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configurBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<PizzaBoxDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaBoxDb"));
            var options = optionsBuilder.Options;
            return new PizzaBoxDbContext(options);
        }

        public static ICrustTypeRepository<CrustType> CreateCrustTypeRepository()
        {
            var db = GetDbContext();
            return new CrustTypeRepository(db);
        }

        public static IOrderRepository<Order> CreateOrderRepository()
        {
            var db = GetDbContext();
            return new OrderRepository(db);
        }

        public static IPizzasSoldRepository<PizzaSold> CreatePizzasSoldRepository()
        {
            var db = GetDbContext();
            return new PizzasSoldRepository(db);
        }

        public static IPresetPizzaRepository<PresetPizza> CreatePresetPizzaRespository()
        {
            var db = GetDbContext();
            return new PresetPizzaRepository(db);
        }

        public static ISizeRepository<Size> CreateSizeRepository()
        {
            var db = GetDbContext();
            return new SizeRepository(db);
        }

        public static IStoreRepository<Store> CreateStoreRepository()
        {
            var db = GetDbContext();
            return new StoreRepository(db);
        }

        public static IUserRepository<User> CreateUserRepository()
        {
            var db = GetDbContext();
            return new UserRepository(db);
        }
    }
}

using System;
using System.Linq;

using PizzaBox.Domain;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Client
{
    class UserConnection
    {
        ICrustTypeRepository<CrustType> crustRepo;
        IOrderRepository<Order> orderRepo;
        IPizzasSoldRepository<PizzaSold> pizzaSoldRepo;
        IPresetPizzaRepository<PresetPizza> presetRepo;
        ISizeRepository<Size> sizeRepo;
        IStoreRepository<Store> storeRepo;
        IUserRepository<User> userRepo;

        User currentUser;

        void SetupRepositories()
        {
            crustRepo = Dependencies.CreateCrustTypeRepository();
            orderRepo = Dependencies.CreateOrderRepository();
            pizzaSoldRepo = Dependencies.CreatePizzasSoldRepository();
            presetRepo = Dependencies.CreatePresetPizzaRespository();
            sizeRepo = Dependencies.CreateSizeRepository();
            storeRepo = Dependencies.CreateStoreRepository();
            userRepo = Dependencies.CreateUserRepository();
        }

        public UserConnection()
        {
            SetupRepositories();
        }

        public UserConnection(string user, string pass)
        {
            SetupRepositories();

            if (userRepo != null)
            {
                SignInAttempt(user, pass);
            }
        }

        public bool SignInAttempt(string user, string pass)
        {
            Console.WriteLine("Attempting to sign in...");

            if (userRepo != null)
            {
                var authUser = userRepo.GetUsers(user, pass);

                if (authUser != null && authUser.Any())
                {
                    Console.WriteLine($"Welcome, {authUser.First().UserName}!");

                    currentUser = authUser.First();

                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                    return false;
                }
            }
            else
            {
                throw new NullReferenceException("ERROR: User data not found");
            }
        }

        public void ViewOrderHistory()
        {
            if (currentUser != null)
            {
                Console.WriteLine();

                if(currentUser.StoreId != null)
                {
                    Console.WriteLine($"Loading order history for store {currentUser.StoreId}...");
                    ViewStoreOrderHistory();
                }
                else
                {
                    Console.WriteLine("Loading your order history...");
                    ViewUserOrderHistory();
                }
            }
            else
            {
                Console.WriteLine("Please sign in to view your order history");
            }
        }

        public void ViewUserOrderHistory()
        {
            if (orderRepo != null)
            {
                foreach (var order in orderRepo.GetOrders(userId: currentUser.UserId))
                {
                    var storeId = order.StoreId ?? default(int);

                    foreach (var pizza in pizzaSoldRepo.GetPizzasSold(orderId: order.OrderId))
                    {
                        var pizzaSize = pizza.PizzaSize ?? default(int);
                        var pizzaCrust = pizza.PizzaCrust ?? default(int);

                        Console.WriteLine($"{order.OrderId}: {sizeRepo.GetSizes(pizzaSize).First().SizeName}" +
                            $" {pizza.PizzaName} " +
                            $"with {crustRepo.GetCrustTypes(pizzaCrust).First().CrustName} crust - ${pizza.TotalCost} ");
                    }

                    Console.WriteLine($"{storeRepo.GetStores(storeId).First().StoreLocation} - {order.OrderTimestamp}");
                }
            }
            else
            {
                throw new NullReferenceException("ERROR: Order history not found");
            }
        }

        public void ViewStoreOrderHistory()
        {
            if (orderRepo != null)
            {
                foreach (var order in orderRepo.GetOrders(storeId: currentUser.StoreId))
                {
                    foreach (var pizza in pizzaSoldRepo.GetPizzasSold(orderId: order.OrderId))
                    {
                        var pizzaSize = pizza.PizzaSize ?? default(int);
                        var pizzaCrust = pizza.PizzaCrust ?? default(int);

                        Console.WriteLine($"{order.OrderId}: {sizeRepo.GetSizes(pizzaSize).First().SizeName} " +
                            $"{pizza.PizzaName} " +
                            $"with {crustRepo.GetCrustTypes(pizzaCrust).First().CrustName} crust - ${pizza.TotalCost}");
                    }
                }
            }
            else
            {
                throw new NullReferenceException("ERROR: Order data not found");
            }
        }

        public void ViewStoreLocations()
        {
            if (storeRepo != null)
            {
                foreach (var store in storeRepo.GetStores())
                {
                    Console.WriteLine($"Location {store.StoreId}: {store.StoreLocation}");
                }
            }
            else
            {
                throw new NullReferenceException("ERROR: Store location data not found");
            }
        }

        public void ViewSizes()
        {
            if (sizeRepo != null)
            {
                foreach (var size in sizeRepo.GetSizes())
                {
                    Console.WriteLine($"{size.SizeId}: {size.SizeName} - ${size.SizeCost}");
                }
            }
            else
            {
                throw new NullReferenceException("ERROR: Size data not found");
            }
        }

        public int ChooseSize()
        {
            if(sizeRepo != null)
            {
                int sizeChoice;

                do
                {
                    ViewSizes();
                    Console.Write("Please choose the size of your pizza: ");
                    sizeChoice = Convert.ToInt32(Console.ReadLine());

                    if(!sizeRepo.GetSizes(sizeChoice).Any())
                        Console.WriteLine("Invalid size. Please try again.");
                } while (!sizeRepo.GetSizes(sizeChoice).Any());

                return sizeChoice;
            }
            else
            {
                throw new NullReferenceException("ERROR: Size data not found");
            }
        }

        public void ViewCrusts()
        {
            if (crustRepo != null)
            {
                foreach (var crust in crustRepo.GetCrustTypes())
                {
                    Console.WriteLine($"{crust.CrustId}: {crust.CrustName} - ${crust.CrustCost}");
                }
            }
            else
            {
               throw new NullReferenceException("ERROR: Crust data not found");
            }
        }

        public int ChoosePresetPizza()
        {
            if(presetRepo != null)
            {
                int presetChoice;

                do
                {
                    ViewPresetPizzas();
                    Console.Write("Please select a pizza: ");
                    presetChoice = Convert.ToInt32(Console.ReadLine());

                    if(!presetRepo.GetPresetPizzas(presetChoice).Any())
                        Console.WriteLine("Invalid pizza. Please try again.");
                } while (!presetRepo.GetPresetPizzas(presetChoice).Any());

                return presetChoice;
            }
            else
            {
                throw new NullReferenceException("ERROR: Preset pizza data not found");
            }
        }

        public void ViewPresetPizzas()
        {
            if (presetRepo != null)
            {
                foreach (var preset in presetRepo.GetPresetPizzas())
                {
                    Console.WriteLine($"{preset.PresetId}: {preset.PresetName}");
                }
            }
            else
            {
                throw new NullReferenceException("ERROR: Preset pizza data not found");
            }
        }

        public int ChooseCrust()
        {
            if (crustRepo != null)
            {
                int crustChoice;

                do
                {
                    ViewCrusts();
                    Console.Write("Please select a crust: ");
                    crustChoice = Convert.ToInt32(Console.ReadLine());

                    if (!crustRepo.GetCrustTypes(crustChoice).Any())
                        Console.WriteLine("Invalid crust choice. Please try again.");
                } while (!crustRepo.GetCrustTypes(crustChoice).Any());

                return crustChoice;
            }
            else
            {
                throw new NullReferenceException("ERROR: Crust data not found");
            }
        }

        public int ChooseStoreLocation()
        {
            if (storeRepo != null)
            {
                int storeChoice;

                do
                {
                    ViewStoreLocations();
                    Console.Write("Please choose a store location: ");
                    storeChoice = Convert.ToInt32(Console.ReadLine());

                    if (!storeRepo.GetStores(storeChoice).Any())
                        Console.WriteLine("Invalid store choice. Please try again.");
                } while (!storeRepo.GetStores(storeChoice).Any());

                return storeChoice;
            }
            else
            {
                throw new NullReferenceException("ERROR: Store location data not found");
            }
        }

        public bool CanOrderNow(int storeLoc)
        {
            var orders = orderRepo.GetOrders(storeId: storeLoc);
            if(orders.Any())
            {
                var lastOrderTime = orders.First().OrderTimestamp;
                var timeSpan = DateTime.Now - lastOrderTime.Value;

                //Console.WriteLine(timeSpan);

                if (timeSpan.Days < Globals.ORDER_INTERVAL_DAYS)
                {
                    return false;
                }

                return true;
            }

            return true;
        }

        public void CreateOrder()
        {
            if (currentUser != null)
            {
                var crustTypeList = crustRepo.GetCrustTypes();
                var sizeList = sizeRepo.GetSizes();
                var storeList = storeRepo.GetStores();
                var presetList = presetRepo.GetPresetPizzas();

                if (crustTypeList.Any() && presetList.Any() && sizeList.Any() && storeList.Any())
                {
                    int storeChoice = ChooseStoreLocation();
                    
                    if(!CanOrderNow(storeChoice))
                    {
                        Console.WriteLine("Cannot order from the same location twice in 24 hours.");
                        return;
                    }

                    int presetChoice = ChoosePresetPizza();
                    int sizeChoice = ChooseSize();
                    int crustChoice = ChooseCrust();

                    //var pizzaName = presetList.ElementAt(presetChoice - 1).PresetName;
                    //var sizeCost = sizeList.ElementAt(sizeChoice - 1).SizeCost;
                    //var crustCost = crustTypeList.ElementAt(crustChoice - 1).CrustCost;

                    var pizzaName = presetRepo.GetPresetPizzas(presetChoice).First().PresetName;
                    var sizeCost = sizeRepo.GetSizes(sizeChoice).First().SizeCost;
                    var crustCost = crustRepo.GetCrustTypes(crustChoice).First().CrustCost;

                    var newPizza = new PizzaSold
                    {
                        PizzaName = pizzaName,
                        PizzaSize = sizeChoice,
                        PizzaCrust = crustChoice,
                        TotalCost = sizeCost + crustCost
                    };

                    if (newPizza.TotalCost > Globals.MAX_ORDER_COST)
                    {
                        Console.WriteLine($"Your order cannot exceed {Globals.MAX_ORDER_COST}. Please try again.");
                        return;
                    }

                    var newOrder = new Order();

                    newOrder.PizzasSold.Add(Mapper.MapPizzaSold(newPizza));
                    newOrder.UserId = currentUser.UserId;
                    newOrder.StoreId = storeChoice;
                    newOrder.TotalCost = newPizza.TotalCost + (newPizza.TotalCost * Globals.SALES_TAX);

                    currentUser.Orders.Add(Mapper.MapOrder(newOrder));
                    userRepo.ModifyUser(currentUser);
                    orderRepo.AddOrder(newOrder);

                    Console.WriteLine("Thank you for ordering!");
                }
                else
                {
                    Console.WriteLine("Something has gone wrong with the data. Please try again later.");
                }
            }
            else
            {
                Console.WriteLine("Please sign in before we take your order");
            }
        }
    }
}

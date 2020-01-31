using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBoxWebsite.Models;

namespace PizzaBoxWebsite.Controllers
{
    public class OrderController : Controller
    {
        private readonly PizzaBoxDbContext _context;
        private readonly IOrderRepository<Order> _orderRepo;
        private readonly ISizeRepository<Size> _sizeRepo;
        private readonly ICrustTypeRepository<CrustType> _crustRepo;
        private readonly IPizzasSoldRepository<PizzaSold> _pizzaRepo;
        private readonly IStoreRepository<Store> _storeRepo;

        public OrderController(PizzaBoxDbContext context, IOrderRepository<Order> orderRepo, IPizzasSoldRepository<PizzaSold> pizzaRepo,
            ISizeRepository<Size> sizeRepo, ICrustTypeRepository<CrustType> crustRepo, IStoreRepository<Store> storeRepo)
        {
            _context = context;

            _orderRepo = orderRepo;
            _sizeRepo = sizeRepo;
            _crustRepo = crustRepo;
            _pizzaRepo = pizzaRepo;
            _storeRepo = storeRepo;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var pizzaBoxDbContext = _context.Orders.Include(o => o.Store)
                .Include(o => o.User).OrderByDescending(o => o.OrderTimestamp);
            return View(await pizzaBoxDbContext.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var repoOrder = _orderRepo.GetOrders().Where(o => o.OrderId == id).FirstOrDefault();

            var ovm = new OrderViewModel();
            ovm.OrderId = repoOrder.OrderId;
            ovm.StoreId = repoOrder.StoreId;
            ovm.UserId = repoOrder.UserId;
            ovm.OrderTimestamp = repoOrder.OrderTimestamp;
            ovm.TotalCost = repoOrder.TotalCost;

            foreach(var item in _pizzaRepo.GetPizzasSold(orderId:ovm.OrderId))
            {
                ovm.PizzasSold.Add(Mapper.MapPizzaSold(item));
            }

            if (repoOrder == null)
                return NotFound();

            return View(ovm);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreLocation");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName");
            ViewData["PizzaType"] = new SelectList(_context.PresetPizzas, "PresetId", "PresetName");
            ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName");
            ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName");

            return View();
        }

        public ViewResult OrderHistory()
        {
            var orders = (Globals.CurrentUser.StoreId == null) ? _orderRepo.GetOrders(userId: Globals.CurrentUser.UserId)
                                                               : _orderRepo.GetOrders(storeId: Globals.CurrentUser.StoreId);

            var ovm = new List<OrderViewModel>();

            foreach(var item in orders)
            {
                var order = new OrderViewModel();
                order.OrderTimestamp = item.OrderTimestamp;
                order.OrderId = item.OrderId;
                order.StoreId = item.StoreId;
                order.UserId = item.UserId;
                order.TotalCost = item.TotalCost;
                order.PizzasSold = item.PizzasSold;
                ovm.Add(order);
            }

            return View(ovm);
        }

        public IActionResult ConfirmOrder()
        {
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreLocation");
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmOrder(OrderViewModel order)
        {
            //ViewData["StoreLocation"] = new SelectList(_context.Stores, "StoreId", "StoreId", order.StoreId);

            if (ModelState.IsValid)
            {
                var newOrder = new Order();
                newOrder.UserId = Globals.CurrentUser.UserId;
                newOrder.StoreId = order.StoreId /*ViewBag.StoreLocation*/;

                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreLocation", newOrder.StoreId);

                decimal? grossCost = 0.00M;

                foreach (var item in Globals.pizzaList)
                {
                    newOrder.PizzasSold.Add(Mapper.MapPizzaSold(item));
                    grossCost += item.TotalCost;
                }

                decimal? salesTax = grossCost * Globals.SALES_TAX;
                newOrder.TotalCost = grossCost + salesTax;

                EmptyPizzaList();

                if (newOrder.TotalCost > Globals.MAX_ORDER_COST)
                {
                    return View(nameof(ExceedMaxCostWarning));
                }

                if(!CanOrderNow(newOrder.StoreId))
                {
                    return View(nameof(TimespanWarning));
                }

                _orderRepo.AddOrder(newOrder);

                return View("../Home/WelcomeUser");
            }
            else
                return View();
        }

        public void EmptyPizzaList()
        {
            for(int x = 0; x < Globals.pizzaList.Count; x++)
            {
                Globals.pizzaList.RemoveAt(x);
            }
        }

        public ViewResult ExceedMaxCostWarning()
        {
            return View();
        }

        public bool CanOrderNow(int? id)
        {
            var orders = _orderRepo.GetOrders(storeId: id);

            if (orders.Any())
            {
                var lastOrderTime = orders.FirstOrDefault().OrderTimestamp;
                var timeSpan = DateTime.Now - lastOrderTime.Value;

                if (timeSpan.Days < Globals.ORDER_INTERVAL_DAYS)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        public ViewResult TimespanWarning()
        {
            return View();
        }

        public ViewResult CreateOrder()
        {
            return View();
        }

        //public IActionResult CreateOrder(OrderViewModel order)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var newOrder = new Order()
        //            {
        //                StoreId = order.StoreId,
        //                UserId = order.UserId,
        //                PizzasSold = order.PizzasSold
        //            };

        //            _orderRepo.AddOrder(newOrder);

        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //            return View();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public IActionResult AddPizza()
        //{
        //    return View();
        //}

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,StoreId,UserId,TotalCost,OrderTimestamp,PizzaType,PizzaSize,PizzaCrust")] Orders orders)
        {
            //PizzasSold newPizza = new PizzasSold();
            PizzaSold newPizza = new PizzaSold();

            if (ModelState.IsValid)
            {
                //ViewData["StoreId"] = new SelectList(_context.Stores, "StoreLocation", "StoreLocation", orders.StoreId);
                //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", orders.UserId);
                //ViewData["PizzaType"] = new SelectList(_context.PizzasSold, "PresetId", "PresetName", newPizza.PizzaName);
                //ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName", newPizza.PizzaSize);
                //ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName", newPizza.PizzaCrust);

                //newPizza.OrderId = orders.OrderId;

                //orders.PizzasSold.Add(newPizza);
                //orders.TotalCost += newPizza.TotalCost;

                _context.Add(orders);
                _context.Add(Mapper.MapPizzaSold(newPizza));

                //orders.PizzasSold.Add(Mapper.MapPizzaSold(ModelState));

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreLocation", "StoreLocation", orders.Store.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", orders.UserId);
            ViewData["PizzaType"] = new SelectList(_context.PizzasSold, "PresetId", "PresetName", newPizza.PizzaName);
            ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName", newPizza.PizzaSize);
            ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName", newPizza.PizzaCrust);

            //newPizza.Order = orders;
            //newPizza.OrderId = orders.OrderId;

            //orders.PizzasSold.Add(Mapper.MapPizzaSold(newPizza));
            //orders.TotalCost += newPizza.TotalCost;

            return View(orders);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", orders.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", orders.UserId);
            return View(orders);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,StoreId,UserId,TotalCost,OrderTimestamp")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", orders.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", orders.UserId);
            return View(orders);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Store)
                .Include(o => o.User)
                .Include(o => o.PizzasSold)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);

            foreach(var item in orders.PizzasSold)
                _context.PizzasSold.Remove(item);

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}

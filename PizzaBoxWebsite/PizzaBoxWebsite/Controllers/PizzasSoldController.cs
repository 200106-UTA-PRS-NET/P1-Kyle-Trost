using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBoxWebsite.Models;

namespace PizzaBoxWebsite.Controllers
{
    public class PizzasSoldController : Controller
    {
        private readonly PizzaBoxDbContext _context;

        private readonly IPizzasSoldRepository<PizzaSold> _pizzasSoldRepo;
        private readonly ISizeRepository<Size> _sizeRepo;
        private readonly ICrustTypeRepository<CrustType> _crustRepo;

        public PizzasSoldController(PizzaBoxDbContext context, IPizzasSoldRepository<PizzaSold> pizzasSoldRepo, ISizeRepository<Size> sizeRepo, ICrustTypeRepository<CrustType> crustRepo)
        {
            _context = context;

            _pizzasSoldRepo = pizzasSoldRepo;
            _sizeRepo = sizeRepo;
            _crustRepo = crustRepo;
        }

        // GET: PizzasSolds
        public async Task<IActionResult> Index(int id = -1)
        {
            IIncludableQueryable<PizzasSold, Sizes> pizzaBoxDbContext;

            if (id != -1)
            {
                pizzaBoxDbContext = _context.PizzasSold.Include(p => p.Order)
                    .Where(p => p.OrderId == id)
                    .Include(p => p.PizzaName)
                    .Include(p => p.PizzaCrustNavigation)
                    .Include(p => p.PizzaSizeNavigation);
            }
            else
            {
                pizzaBoxDbContext = _context.PizzasSold.Include(p => p.Order)
                    .Include(p => p.PizzaName)
                    .Include(p => p.PizzaCrustNavigation)
                    .Include(p => p.PizzaSizeNavigation);
            }

            return View(await pizzaBoxDbContext.ToListAsync());
        }

        // GET: PizzasSolds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzasSold = await _context.PizzasSold
                .Include(p => p.Order)
                .Include(p => p.PizzaCrustNavigation)
                .Include(p => p.PizzaSizeNavigation)
                .FirstOrDefaultAsync(m => m.PizzaId == id);
            if (pizzasSold == null)
            {
                return NotFound();
            }

            return View(pizzasSold);
        }

        // GET: PizzasSolds/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["PizzaName"] = new SelectList(_context.PresetPizzas, "PresetId", "PresetName");
            ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName");
            ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName");
            return View();
        }

        // POST: PizzasSolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,PizzaName,PizzaSize,PizzaCrust,TotalCost,PizzaId")] PizzasSold pizzasSold)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzasSold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", pizzasSold.OrderId);
            ViewData["PizzaName"] = new SelectList(_context.PresetPizzas, "PresetName", "PresetName", pizzasSold.PizzaName);
            ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName", pizzasSold.PizzaCrust);
            ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName", pizzasSold.PizzaSize);
            return View(pizzasSold);
        }

        public IActionResult CreatePizzaPartial()
        {
            ViewData["PizzaName"] = new SelectList(_context.PresetPizzas, "PresetName", "PresetName");
            ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName");
            ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName");
            return View();
        }

        [HttpPost]
        public IActionResult CreatePizzaPartial(PizzaSoldViewModel pizza)
        {
            if (ModelState.IsValid)
            {
                var newPizza = new PizzaSold();
                newPizza.PizzaName = pizza.PizzaName;
                newPizza.PizzaSize = pizza.PizzaSize;
                newPizza.PizzaCrust = pizza.PizzaCrust;

                ViewData["PizzaName"] = new SelectList(_context.PresetPizzas, "PresetName", "PresetName", newPizza.PizzaName);

                newPizza.TotalCost = CalculateTotalCost(newPizza.PizzaSize, newPizza.PizzaCrust);

                Globals.pizzaList.Add(newPizza);

                return RedirectToAction(nameof(PizzaAdded));
            }
            else
                return View();
        }

        private decimal CalculateTotalCost(int? size, int? crust)
        {
            var sizeCost = _sizeRepo.GetSizes(size).FirstOrDefault().SizeCost;
            var crustCost = _crustRepo.GetCrustTypes(crust).FirstOrDefault().CrustCost;

            return sizeCost + crustCost;
        }

        public IActionResult PizzaAdded()
        {
            return View();
        }

        // GET: PizzasSolds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzasSold = await _context.PizzasSold.FindAsync(id);
            if (pizzasSold == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", pizzasSold.OrderId);
            ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName", pizzasSold.PizzaCrust);
            ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName", pizzasSold.PizzaSize);
            return View(pizzasSold);
        }

        // POST: PizzasSolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,PizzaName,PizzaSize,PizzaCrust,TotalCost,PizzaId")] PizzasSold pizzasSold)
        {
            if (id != pizzasSold.PizzaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzasSold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzasSoldExists(pizzasSold.PizzaId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", pizzasSold.OrderId);
            ViewData["PizzaCrust"] = new SelectList(_context.CrustTypes, "CrustId", "CrustName", pizzasSold.PizzaCrust);
            ViewData["PizzaSize"] = new SelectList(_context.Sizes, "SizeId", "SizeName", pizzasSold.PizzaSize);
            return View(pizzasSold);
        }

        // GET: PizzasSolds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzasSold = await _context.PizzasSold
                .Include(p => p.Order)
                .Include(p => p.PizzaCrustNavigation)
                .Include(p => p.PizzaSizeNavigation)
                .FirstOrDefaultAsync(m => m.PizzaId == id);
            if (pizzasSold == null)
            {
                return NotFound();
            }

            return View(pizzasSold);
        }

        // POST: PizzasSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizzasSold = await _context.PizzasSold.FindAsync(id);
            _context.PizzasSold.Remove(pizzasSold);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzasSoldExists(int id)
        {
            return _context.PizzasSold.Any(e => e.PizzaId == id);
        }
    }
}

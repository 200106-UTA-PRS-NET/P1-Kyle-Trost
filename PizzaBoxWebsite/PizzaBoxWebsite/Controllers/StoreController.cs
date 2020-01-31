using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using PizzaBoxWebsite.Models;

namespace PizzaBoxWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly PizzaBoxDbContext _context;

        private readonly IStoreRepository<Store> _storeRepo;

        public StoreController(PizzaBoxDbContext context)
        {
            _context = context;

            _storeRepo = Dependencies.CreateStoreRepository();
        }

        // GET: api/Stores
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Stores>>> GetStores()
        //{
        //    return await _context.Stores.ToListAsync();
        //}

        //[HttpGet]
        //public IEnumerable<Store> GetAllStores()
        //{
        //    return _storeRepo.GetStores();
        //}
        //[HttpGet]
        public ActionResult StoreLocations()
        {
            var svm = new List<StoreViewModel>();
            var stores = _storeRepo.GetStores();

            foreach(var item in stores)
            {
                var store = new StoreViewModel();
                store.StoreId = item.StoreId;
                store.StoreLocation = item.StoreLocation;
                svm.Add(store);
            }

            return View(svm);
            //return View(_storeRepo.GetStores());
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stores>> GetStores(int id)
        {
            var stores = await _context.Stores.FindAsync(id);

            if (stores == null)
            {
                return NotFound();
            }

            return stores;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStores(int id, Stores stores)
        {
            if (id != stores.StoreId)
            {
                return BadRequest();
            }

            _context.Entry(stores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Stores>> PostStores(Stores stores)
        {
            _context.Stores.Add(stores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStores", new { id = stores.StoreId }, stores);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stores>> DeleteStores(int id)
        {
            var stores = await _context.Stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(stores);
            await _context.SaveChangesAsync();

            return stores;
        }

        private bool StoresExists(int id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }
    }
}

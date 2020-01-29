using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBoxWebsite.Models;

namespace PizzaBoxWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserRepository<User> _userRepo;
        private readonly IStoreRepository<Store> _storeRepo;

        private readonly SignInManager<IdentityUser> signInManager;

        public string Username { get; set; }
        public string Password { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            _userRepo = Dependencies.CreateUserRepository();
            _storeRepo = Dependencies.CreateStoreRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult GoToSignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(string userName, string passWord)
        {
            if(_userRepo.GetUsers(user: userName, pass: passWord).Any())
            {
                //return View(userName, pass);
                return View("WelcomeUser");
            }

            return View();
        }

        public ViewResult StoreLocations()
        {
            return View("StoreLocations", _storeRepo.GetStores());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

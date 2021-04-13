using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TownerTown.Web.Models;
using Microsoft.AspNetCore.Http;
using TownerTown.Service;
using TownerTown.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using TownerTown.Helpers;

namespace TownerTown.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment hostingEnvironment;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly ILogger<HomeController> _logger;

        private IUserService _userService;
        private IBusinessService _businessService;
        public HomeController(IUserService userService, IBusinessService businessService, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userService = userService;
            _businessService = businessService;
            hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            
            List<string> Cities = new List<string>();
            Cities = _businessService.GetALLCities();
           // ViewBag.Cities = Cities
           //.Select(c => new SelectListItem() { Text = c, Value = c })
           //.ToList();
            List<City> cities = new List<City>();
            int i = 0;
            foreach (var city in Cities)
            {
                City newCity = new City { CityID = city, CityName = city };
                cities.Add(newCity);
                i++;
            }
            searchViewModel.Cities = cities;

            List<Category> categories = new List<Category>();
            categories = _businessService.GetAllCategories();
            searchViewModel.Categories = categories;
            ViewBag.DefaultLayout = true;
            var userAgent = HttpContext.Request.Headers["User-Agent"];

            if (DeviceIdentofication.IsMobileDevice(userAgent))
            {
                _session.SetInt32("IsMobileDevice", 1);
                // Return mobile view
            }
            return View(searchViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            if (TempData.ContainsKey("Message"))
            {
                if (TempData["Message"].ToString() == "Error")
                {
                    ViewBag.Error = "UseName Password Is Incorrect";

                }
            }


            return View();
        }
        public IActionResult LogOut()
        {
            _session.Clear();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public List<string> GetAllUserNames()
        {
            return _userService.GetAllUserNames();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using ShopMvcApp_PD211.Models;
using System.Diagnostics;

namespace ShopMvcApp_PD211.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatalogDbContext context;

        public HomeController(CatalogDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            // ... load data from database ...
            var products = context.Mercedeses
                .Include(x => x.BrandOfCar) // LEFT JOIN
                .ToList();

            return View(products); // search for view in: ~/Views/Home/Index
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            // your logic...
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

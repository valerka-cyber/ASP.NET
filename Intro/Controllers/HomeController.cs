using Intro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Intro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var model = new AboutModel
            {
                Data = "The Model Data"
            };
            return View(model);
        }

        public IActionResult Contacts()
        {
            var model = new ContactsModel
            {
                Name = "Valeriia",
                numberPhohe = "+380983383672",
                adress = "st.Primorskaya"
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
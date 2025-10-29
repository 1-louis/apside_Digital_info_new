using Digital_info.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Digital_info.Controllers
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
			Database.Connect();
            /*Services serv = new Services();*/
           
            ViewData["ExperienceTop6"] = new Experience_DAO().getBestByNBR(6); // affichage les 6 mieux notés 

			Database.Close();
   

			return View();
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
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP1_webApp.Models;

namespace TP1_webApp.Controllers
{
    public class HomeController : Controller
    {
        // HomeController
        private readonly ILogger<HomeController> _logger;


        // Logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        // Idex view
        public IActionResult Index()
        {
            return View();
        }


        // Privacy view
        public IActionResult Privacy()
        {
            SQLConnection myServer = new SQLConnection();
            // ... calling the model method
            myServer.Get();
            return View(myServer);
        }

        // Error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public ActionResult Get_Items()
        {
            SQLConnection myModel = new SQLConnection();
            // ... calling the model method
            myModel.Get();
            return View("Privacy", myModel);
        }
    }
}
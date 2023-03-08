using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;
using TP1_webApp.Models;

namespace TP1_webApp.Controllers
{
    public class HomeController : Controller
    {
        // HomeController
        private readonly ILogger<HomeController> _logger;
        SQLConnection myConnection = new SQLConnection();

        // Logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        // Index view
        public IActionResult Index()
        {
            return View();
        }


        // Return to Privacy view
        public IActionResult Return()
        {
            // ... calling the model method
            myConnection.Get();
            return View("Privacy", myConnection);
        }

        // Insert view
        public IActionResult Insert()
        {
            return View("Insert", myConnection);
        }

        // Privacy view
        public IActionResult Privacy()
        {
            // ... calling the model method
            myConnection.Get();
            return View(myConnection);
        }

        // Error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Get items method
        public ActionResult Get_Items()
        {
            // ... calling the Get method
            myConnection.Get();
            return View("Privacy", myConnection);
        }

        // Add items method
        public ActionResult Add_Items()
        {
            myConnection.Add();
            return View("Insert", myConnection);
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AllCars.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
//using AjaxWebApp.Models;

namespace AllCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CarsContext db = new CarsContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       [HttpGet]
        public ActionResult SearchResult(Search searchparams)
        {
            GetCarsAvtoria car = new GetCarsAvtoria();
            List<GetCarsAvtoria> listCars = car.ListCarsAvtoria(searchparams);

            ViewBag.Cars11 = listCars;

            string s = searchparams.Mark; 
            return View();
        }
        public IActionResult Index()
        {

            
            

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

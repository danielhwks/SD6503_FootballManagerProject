using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FootballStatisticsManagementApp.Models;
using FootballStatisticsManagementApp.Utilities;

namespace FootballStatisticsManagementApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!LoginUtility.CheckAuthenticated(HttpContext.Session))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult About()
        {
            if (!LoginUtility.CheckAuthenticated(HttpContext.Session))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["Message"] = "MicroManage your teams";

            return View();
        }

        public IActionResult Contact()
        {
            if (!LoginUtility.CheckAuthenticated(HttpContext.Session))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["Message"] = "Address";

            return View();
        }

        public IActionResult Privacy()
        {
            if (!LoginUtility.CheckAuthenticated(HttpContext.Session))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

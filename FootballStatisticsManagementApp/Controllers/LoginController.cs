using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FootballStatisticsManagementApp.Models;

namespace FootballStatisticsManagementApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate([FromForm] UserLogin details)
        {
            // Read credentials from file
            string[] credentials = System.IO.File.ReadAllLines("cred.txt");
            var credUsername = credentials[0];
            var credPassword = credentials[1];

            // Check that credentials match user input
            if (details.username == credUsername && details.password == credPassword)
            {
                // Authenticate user session
                HttpContext.Session.SetInt32("auth", 1);
                return RedirectToAction("Index", "Home");
            }

            // Else return the login index
            return View("Index");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Unauthenticate user session
            HttpContext.Session.SetInt32("auth", 0);
            return View("Index");
        }
    }
}
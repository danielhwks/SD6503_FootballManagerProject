using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLogin.Models;

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
        public IActionResult Authenticate(string username, string password)
        {
            // Read credentials from file
            string[] credentials = System.IO.File.ReadAllLines("cred.txt");
            var credUsername = credentials[0];
            var credPassword = credentials[1];
            Console.WriteLine(credUsername);
            if (username == credUsername && password == credPassword)
            {
                HttpContext.Session.SetInt32("auth", 1);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
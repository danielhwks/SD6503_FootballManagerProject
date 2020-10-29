using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FootballStatisticsManagementApp.Models;
using FootballStatisticsManagementApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FootballStatisticsManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext _context;

        public HomeController(HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!LoginUtility.CheckAuthenticated(HttpContext.Session))
            {
                return RedirectToAction("Index", "Login");
            }

            var _ = _context.Stats.ToList();
            var _2 = _context.Team.ToList();
            var stats = _context.Stats.Include(s => s.Player.Team)
                .GroupBy(i => i.Player)
                .Select(g => new { Player = g.Key, Team = g.Key.Team, Total = g.Sum(j => j.Goals) })
                .OrderByDescending(g => g.Total).Take(3);

            //ViewBag.topPlayers = stats.ToList();
            List<(Player, Team, int)> realStats = new List<(Player, Team, int)>();
            foreach (var stat in stats.ToList())
            {
                realStats.Add((stat.Player, stat.Team, (int)stat.Total));
            }

            ViewBag.topPlayers = realStats.ToList();

            foreach (var stat in realStats)
            {
                Console.WriteLine(stat.Item1.Name);
                Console.WriteLine(stat.Item1.Team.Name);
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

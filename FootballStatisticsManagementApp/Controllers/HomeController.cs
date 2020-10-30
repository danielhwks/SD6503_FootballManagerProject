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
            // Ensures that the user is logged in and if not redirects to the login page
            if (!LoginUtility.CheckAuthenticated(HttpContext.Session))
            {
                return RedirectToAction("Index", "Login");
            }

            var _stats = _context.Stats.ToList();
            var _teams = _context.Team.ToList();

            // Fetch the 3 players with the most goals
            var stats = _context.Stats.Include(s => s.Player.Team)
                .GroupBy(i => i.Player)
                .Select(g => new { Player = g.Key, Team = g.Key.Team, Total = g.Sum(j => j.Goals) })
                .OrderByDescending(g => g.Total).Take(3);
            
            // Convert from a dynamic type to a tuple (because of errors)
            List<(Player, Team, int)> realStats = new List<(Player, Team, int)>();
            foreach (var stat in stats.ToList())
            {
                realStats.Add((stat.Player, stat.Team, (int)stat.Total));
            }
            // Add to view
            ViewBag.topPlayers = realStats.ToList();

            // Fetch the teams with the most goals
            var teamStats = _context.Stats.Include(s => s.Player).Include(s => s.Player.Team)
                .GroupBy(i => i.Player.Team)
                .Select(g => new { Team = g.Key, Total = g.Sum(j => j.Player.Stats.Sum(k => k.Goals)) })
                .OrderByDescending(g => g.Total).Take(3);

            // Convert from a dynamic type to a tuple (because of errors)
            List<(Team, int)> realTeamStats = new List<(Team, int)>();
            foreach (var stat in teamStats.ToList())
            {
                realTeamStats.Add((stat.Team, (int)stat.Total));
            }
            // Add to view
            ViewBag.topTeams = realTeamStats.ToList();

            return View();
        }

        public IActionResult About()
        {
            // Ensures that the user is logged in and if not redirects to the login page
            if (!LoginUtility.CheckAuthenticated(HttpContext.Session))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["Message"] = "MicroManage your teams";

            return View();
        }

        public IActionResult Contact()
        {
            // Ensures that the user is logged in and if not redirects to the login page
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

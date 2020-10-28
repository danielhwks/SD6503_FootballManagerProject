using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballStatisticsManagementApp.Models;

namespace FootballStatisticsManagementApp.Controllers
{
    public class PlayersController : Controller
    {
        private readonly HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext _context;

        public PlayersController(HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(string sortParam, string searchParam)
        {
            var players = from p in _context.Player.Include(p => p.Team) select p;
            if (!String.IsNullOrEmpty(searchParam))
            {
                players = players.Where(p => p.Name.Contains(searchParam));
            }
            switch (sortParam)
            {
                case "name_asc":
                    players = players.OrderBy(p => p.Name);
                    ViewBag.sortBy = "name_asc";
                    break;
                case "name_desc":
                    players = players.OrderByDescending(p => p.Name);
                    ViewBag.sortBy = "name_desc";
                    break;
                case "dob_asc":
                    players = players.OrderBy(p => p.Dob);
                    ViewBag.sortBy = "dob_asc";
                    break;
                case "dob_desc":
                    players = players.OrderByDescending(p => p.Dob);
                    ViewBag.sortBy = "dob_desc";
                    break;
                case "kit_asc":
                    players = players.OrderBy(p => p.KitNumber);
                    ViewBag.sortBy = "kit_asc";
                    break;
                case "kit_desc":
                    players = players.OrderByDescending(p => p.KitNumber);
                    ViewBag.sortBy = "kit_desc";
                    break;
                case "team_asc":
                    players = players.OrderBy(p => p.Team);
                    ViewBag.sortBy = "team_asc";
                    break;
                case "team_desc":
                    players = players.OrderByDescending(p => p.Team);
                    ViewBag.sortBy = "team_desc";
                    break;
                default:
                    players = players.OrderBy(p => p.Name);
                    ViewBag.sortBy = "name_asc";
                    break;
            }
            return View(await players.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }
            var _ = _context.Stats.Where(s => s.PlayerId == player.PlayerId).ToList();
            ViewBag.goals = player.Stats.Sum(s => s.Goals);
            //Console.WriteLine(stats.Count);
            ViewBag.assists = player.Stats.Sum(s => s.Assists);
            ViewBag.saves = player.Stats.Sum(s => s.Saves);
            Console.WriteLine(ViewBag.goals);

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,Name,Dob,KitNumber,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "Name", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,Name,Dob,KitNumber,TeamId")] Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Player.FindAsync(id);
            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.PlayerId == id);
        }
    }
}

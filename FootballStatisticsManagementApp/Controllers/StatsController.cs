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
    public class StatsController : Controller
    {
        private readonly HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext _context;

        public StatsController(HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext context)
        {
            _context = context;
        }

        // GET: Stats
        public async Task<IActionResult> Index(string sortParam = "", string searchParam = "")
        {
            var stats = from p in _context.Stats.Include(s => s.Player).Include(s => s.Match) select p;

            // If the user has searched then filter the objects
            if (!String.IsNullOrEmpty(searchParam))
            {
                stats = stats.Where(t => t.Player.Name.Contains(searchParam));
            }

            // If the user has sorted then sort the objects by the selected field
            switch (sortParam)
            {
                case "goals_asc":
                    stats = stats.OrderBy(t => t.Goals);
                    ViewBag.sortBy = "goals_asc";
                    break;
                case "goals_desc":
                    stats = stats.OrderByDescending(t => t.Goals);
                    ViewBag.sortBy = "goals_desc";
                    break;
                case "assists_asc":
                    stats = stats.OrderBy(t => t.Assists);
                    ViewBag.sortBy = "assists_asc";
                    break;
                case "assists_desc":
                    stats = stats.OrderByDescending(t => t.Assists);
                    ViewBag.sortBy = "assists_desc";
                    break;
                case "saves_asc":
                    stats = stats.OrderBy(t => t.Saves);
                    ViewBag.sortBy = "saves_asc";
                    break;
                case "saves_desc":
                    stats = stats.OrderByDescending(t => t.Saves);
                    ViewBag.sortBy = "saves_desc";
                    break;
                case "name_asc":
                    stats = stats.OrderBy(t => t.Player);
                    ViewBag.sortBy = "name_asc";
                    break;
                case "name_desc":
                    stats = stats.OrderByDescending(t => t.Player);
                    ViewBag.sortBy = "name_desc";
                    break;
                case "date_asc":
                    stats = stats.OrderBy(t => t.Match);
                    ViewBag.sortBy = "date_asc";
                    break;
                case "date_desc":
                    stats = stats.OrderByDescending(t => t.Match);
                    ViewBag.sortBy = "date_desc";
                    break;
                default:
                    break;
            }

            return View(await stats.ToListAsync());
        }

        // GET: Stats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Shows the details of the object
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats
                .FirstOrDefaultAsync(m => m.StatsId == id);
            if (stats == null)
            {
                return NotFound();
            }

            return View(stats);
        }

        // GET: Stats/Create
        public IActionResult Create()
        {
            // Returns the Create view of the object type
            ViewData["MatchId"] = new SelectList(_context.Match, "MatchId", "Date");
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "Name");
            return View();
        }

        // POST: Stats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatsId,Goals,Assists,Saves,MatchId,PlayerId")] Stats stats)
        {
            // Validates and creates the object based on the given fields
            if (ModelState.IsValid)
            {
                _context.Add(stats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stats);
        }

        // GET: Stats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns the Edit view of the object
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats.FindAsync(id);
            if (stats == null)
            {
                return NotFound();
            }
            ViewData["MatchId"] = new SelectList(_context.Match, "MatchId", "Date", stats.MatchId);
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "Name", stats.PlayerId);
            return View(stats);
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatsId,Goals,Assists,Saves,MatchId,PlayerId")] Stats stats)
        {
            // Validates and edits the object
            if (id != stats.StatsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatsExists(stats.StatsId))
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
            return View(stats);
        }

        // GET: Stats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Returns the Delete view
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats
                .FirstOrDefaultAsync(m => m.StatsId == id);
            if (stats == null)
            {
                return NotFound();
            }

            return View(stats);
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Deletes the given object
            var stats = await _context.Stats.FindAsync(id);
            _context.Stats.Remove(stats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatsExists(int id)
        {
            return _context.Stats.Any(e => e.StatsId == id);
        }
    }
}

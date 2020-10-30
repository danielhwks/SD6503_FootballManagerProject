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
    public class LeaguesController : Controller
    {
        private readonly HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext _context;

        public LeaguesController(HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext context)
        {
            _context = context;
        }

        // GET: Leagues
        public async Task<IActionResult> Index(string sortParam = "", string searchParam = "")
        {
            // Fetches all League objects from the database
            var leagues = from l in _context.League select l;

            // If the user has searched then filter the objects
            if (!String.IsNullOrEmpty(searchParam))
            {
                leagues = leagues.Where(l => l.Year.Contains(searchParam));
            }

            // If the user has sorted then sort the objects by the selected field
            switch (sortParam)
            {
                case "year_asc":
                    leagues = leagues.OrderBy(l => l.Year);
                    ViewBag.sortBy = "year_asc";
                    break;
                case "year_desc":
                    leagues = leagues.OrderByDescending(l => l.Year);
                    ViewBag.sortBy = "year_desc";
                    break;
                default:
                    break;
            }

            // Returned the remaining objects
            return View(await leagues.ToListAsync());
        }

        // GET: Leagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Shows the details of the object
            if (id == null)
            {
                return NotFound();
            }

            var league = await _context.League
                .FirstOrDefaultAsync(m => m.LeagueId == id);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // GET: Leagues/Create
        public IActionResult Create()
        {
            // Returns the Create view of the object type
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeagueId,Year")] League league)
        {
            // Validates and creates the object based on the given fields
            if (ModelState.IsValid)
            {
                _context.Add(league);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(league);
        }

        // GET: Leagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns the Edit view of the object
            if (id == null)
            {
                return NotFound();
            }

            var league = await _context.League.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }
            return View(league);
        }

        // POST: Leagues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeagueId,Year")] League league)
        {
            // Validates and edits the object
            if (id != league.LeagueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(league);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueExists(league.LeagueId))
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
            return View(league);
        }

        // GET: Leagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Returns the Delete view
            if (id == null)
            {
                return NotFound();
            }

            var league = await _context.League
                .FirstOrDefaultAsync(m => m.LeagueId == id);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Deletes the given object
            var league = await _context.League.FindAsync(id);
            _context.Match.RemoveRange(_context.Match.Where(m => m.League == league));
            _context.League.Remove(league);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueExists(int id)
        {
            return _context.League.Any(e => e.LeagueId == id);
        }
    }
}

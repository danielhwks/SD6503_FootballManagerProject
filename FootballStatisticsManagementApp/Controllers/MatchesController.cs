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
    public class MatchesController : Controller
    {
        private readonly HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext _context;

        public MatchesController(HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index(string sortParam = "", string searchParam = "")
        {
            // Fetches all Match objects from the database
            var matches = from m in _context.Match.Include(m => m.AwayTeam).Include(m => m.HomeTeam).Include(m => m.League) select m;

            // If the user has searched then filter the objects
            if (!String.IsNullOrEmpty(searchParam))
            {
                matches = matches.Where(m => m.Location.Contains(searchParam));
            }

            // If the user has sorted then sort the objects by the selected field
            switch (sortParam)
            {
                case "loc_asc":
                    matches = matches.OrderBy(m => m.Location);
                    ViewBag.sortBy = "loc_asc";
                    break;
                case "loc_desc":
                    matches = matches.OrderByDescending(m => m.Location);
                    ViewBag.sortBy = "loc_desc";
                    break;
                case "date_asc":
                    matches = matches.OrderBy(m => m.Date);
                    ViewBag.sortBy = "date_asc";
                    break;
                case "date_desc":
                    matches = matches.OrderByDescending(m => m.Date);
                    ViewBag.sortBy = "date_desc";
                    break;
                case "away_asc":
                    matches = matches.OrderBy(m => m.AwayTeam);
                    ViewBag.sortBy = "away_asc";
                    break;
                case "away_desc":
                    matches = matches.OrderByDescending(m => m.AwayTeam);
                    ViewBag.sortBy = "away_desc";
                    break;
                case "home_asc":
                    matches = matches.OrderBy(m => m.HomeTeam);
                    ViewBag.sortBy = "home_asc";
                    break;
                case "home_desc":
                    matches = matches.OrderByDescending(m => m.HomeTeam);
                    ViewBag.sortBy = "home_desc";
                    break;
                case "leag_asc":
                    matches = matches.OrderBy(m => m.League);
                    ViewBag.sortBy = "leag_asc";
                    break;
                case "leag_desc":
                    matches = matches.OrderByDescending(m => m.League);
                    ViewBag.sortBy = "leag_desc";
                    break;
                default:
                    break;
            }

            // Returned the remaining objects
            return View(await matches.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Shows the details of the object
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.League)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            // Returns the Create view of the object type
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Year");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,Location,Date,LeagueId,HomeTeamId,AwayTeamId")] Match match)
        {
            // Validates and creates the object based on the given fields
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.HomeTeamId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Year", match.LeagueId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns the Edit view of the object
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.HomeTeamId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Year", match.LeagueId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchId,Location,Date,LeagueId,HomeTeamId,AwayTeamId")] Match match)
        {
            // Validates and edits the object
            if (id != match.MatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.MatchId))
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
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.HomeTeamId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Year", match.LeagueId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Returns the Delete view
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.League)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Deletes the given object
            var match = await _context.Match.FindAsync(id);
            _context.Match.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.MatchId == id);
        }
    }
}

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
    public class TeamsController : Controller
    {
        private readonly HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext _context;

        public TeamsController(HSD6503_ProjectSD6503_Project_DBDatabaseFSMDBmdfContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index(string sortParam = "", string searchParam = "")
        {

            // Fetches all Team objects from the database
            var teams = from t in _context.Team select t;

            // If the user has searched then filter the objects
            if (!String.IsNullOrEmpty(searchParam))
            {
                teams = teams.Where(t => t.Name.Contains(searchParam));
            }

            // If the user has sorted then sort the objects by the selected field
            switch (sortParam)
            {
                case "name_asc":
                    teams = teams.OrderBy(t => t.Name);
                    ViewBag.sortBy = "name_asc";
                    break;
                case "name_desc":
                    teams = teams.OrderByDescending(t => t.Name);
                    ViewBag.sortBy = "name_desc";
                    break;
                case "loc_asc":
                    teams = teams.OrderBy(t => t.Location);
                    ViewBag.sortBy = "loc_asc";
                    break;
                case "loc_desc":
                    teams = teams.OrderByDescending(t => t.Location);
                    ViewBag.sortBy = "loc_desc";
                    break;
                default:
                    break;
            }

            // Returned the remaining objects
            return View(await teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Shows the details of the object
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            // Returns the Create view of the object type
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name,Location")] Team team)
        {
            // Validates and creates the object based on the given fields
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns the Edit view of the object
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name,Location")] Team team)
        {
            // Validates and edits the object
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Returns the Delete view
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Deletes the given object
            var team = await _context.Team.FindAsync(id);
            _context.Match.RemoveRange(_context.Match.Where(m => m.AwayTeam == team || m.HomeTeam == team));
            _context.Player.RemoveRange(_context.Player.Where(p => p.Team == team));
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamId == id);
        }
    }
}

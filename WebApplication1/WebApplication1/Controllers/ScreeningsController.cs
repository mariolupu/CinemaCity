using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")] // Doar Administratorii pot gestiona orarele proiecțiilor
    public class ScreeningsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScreeningsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Screenings
        public async Task<IActionResult> Index()
        {
            var screenings = await _context.Screenings
                .Include(s => s.Movie)
                .OrderBy(s => s.Showtime)
                .ToListAsync();

            return View(screenings);
        }

        // GET: Screenings/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title");
            return View();
        }

        // POST: Screenings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,Showtime,HallName")] Screening screening)
        {
            if (ModelState.IsValid)
            {
                _context.Add(screening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", screening.MovieId);
            return View(screening);
        }

        // POST: Screenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening != null)
            {
                _context.Screenings.Remove(screening);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
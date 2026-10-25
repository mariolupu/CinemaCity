
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Barem: Nota 2 - Panou de management pentru administrator
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Admin()
    {
        // Verificăm siguranța bazei de date și returnăm o listă (chiar și goală, dar niciodată null)
        var movies = _context.Movies != null ? await _context.Movies.ToListAsync() : new List<Movie>();
        return View(movies);
    }

    // GET: MOVIES

    // ??? Barem: Nota 2 - Catalogul public de filme cu verificare de siguranță pentru Model
    public async Task<IActionResult> Index()
    {
        // Verificăm dacă contextul și setul de filme sunt inițializate, altfel trimitem o listă goală
        var movies = _context.Movies != null ? await _context.Movies.ToListAsync() : new List<Movie>();
        return View(movies);
    }

    // GET: MOVIES/Details/5
    // GET: Movies/Details/5
    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null) return NotFound();

        // Preluăm proiecțiile setate de admin din baza de date
        var screenings = await _context.Screenings
            .Where(s => s.MovieId == movie.Id)
            .OrderBy(s => s.Showtime)
            .ToListAsync();

        ViewBag.Screenings = screenings;
        return View(movie);
    }

    // GET: MOVIES/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Id,Title,Genre,Description,Duration,ImageUrl")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Genre,Description,Duration,ImageUrl")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
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
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}

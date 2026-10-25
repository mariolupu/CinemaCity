using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Bookings/Book/5 (id-ul este ScreeningId)
        public async Task<IActionResult> Book(int id)
        {
            var screening = await _context.Screenings
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (screening == null)
            {
                return NotFound();
            }

            // Găsim toate locurile deja rezervate pentru această proiecție
            var existingBookings = await _context.Bookings
                .Where(b => b.ScreeningId == id)
                .ToListAsync();

            var bookedSeats = new List<string>();
            foreach (var b in existingBookings)
            {
                if (!string.IsNullOrEmpty(b.SeatNumbers))
                {
                    var seats = b.SeatNumbers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var seat in seats)
                    {
                        bookedSeats.Add(seat.Trim());
                    }
                }
            }

            ViewBag.BookedSeats = bookedSeats;
            return View(screening);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int screeningId, string selectedSeats)
        {
            if (string.IsNullOrEmpty(selectedSeats))
            {
                TempData["Error"] = "Please select at least one seat!";
                return RedirectToAction("Book", new { id = screeningId });
            }

            var userId = _userManager.GetUserId(User);

            var booking = new Booking
            {
                ScreeningId = screeningId,
                UserId = userId,
                SeatNumbers = selectedSeats,
                BookingDate = DateTime.Now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation", new { id = booking.Id });
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Screening)
                .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }
    }
}
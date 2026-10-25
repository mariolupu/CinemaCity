using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        // Barem: Nota 2, Criteriul 01 - Proiectarea bazei de date (Adăugarea tabelului de filme)
        public DbSet<WebApplication1.Models.Movie> Movies { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Screening> Screenings { get; set; }
    }
}

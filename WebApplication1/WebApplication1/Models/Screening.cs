using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Screening
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Required]
        public DateTime Showtime { get; set; } // Data și ora proiecției

        public string HallName { get; set; } = "Main Cinema??? (IMAX)";
    }
}
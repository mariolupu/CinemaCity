using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int ScreeningId { get; set; }
        public Screening? Screening { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string SeatNumbers { get; set; } = string.Empty; // ex: "A1, A2"

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
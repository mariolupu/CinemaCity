using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Promotion
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Badge / Type")]
        public string BadgeText { get; set; } // e.g. "🎓 STUDENT / YOUTH", "🔥 PROMO DAY", "✨ VIP"

        [Required]
        public string Title { get; set; } // e.g. "Student Menu: Popcorn + Soda"

        public string Subtitle { get; set; } // e.g. "Valid on weekdays with student ID"

        [Required]
        public string Description { get; set; } // e.g. "Show your student card at the bar to claim 20% off!"

        [Display(Name = "Discount Percentage")]
        public int? DiscountPercentage { get; set; } // e.g. 20 (for 20%)

        [Display(Name = "Fixed Price (RON)")]
        public decimal? FixedPrice { get; set; } // e.g. 15 (for 15 LEI)

        [Display(Name = "Applicable Days")]
        public string ApplicableDays { get; set; } // e.g. "Monday, Tuesday" or "Weekend"

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
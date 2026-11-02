namespace WebApplication1.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string? ImageUrl { get; set; }

        // Câmpul pentru descriere
        public string? Description { get; set; }

        public bool IsFeatured { get; set; } = false;

        // Câmp nou: Rândul/Secțiunea de pe pagina principală (controlat de admin)
        // Exemplu valori: "Comedii", "SF & Action", "Trending"
        public string? HomeSection { get; set; }
    }
}
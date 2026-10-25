namespace WebApplication1.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Title { get; set; }           // Exemplu: "Meniul Studentului 🍿"
        public string Description { get; set; }     // Exemplu: "Ia-ți popcorn mediu și suc la doar 15 lei pe baza carnetului!"
        public string BadgeText { get; set; }       // Exemplu: "🔥 OFERTĂ SPECIALĂ"
        public string ImageUrl { get; set; }        // Link-ul imaginii de fundal
        public bool IsActive { get; set; } = true;  // Să fie vizibil în carusel sau nu
    }
}
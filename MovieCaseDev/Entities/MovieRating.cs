using System.ComponentModel.DataAnnotations;

namespace MovieCaseDev.Entities
{
    public class MovieRating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string UserEmail { get; set; }
        public string? Note { get; set; }
        [Range(1, 10)]
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

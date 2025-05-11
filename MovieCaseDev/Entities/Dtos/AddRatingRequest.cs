using System.ComponentModel.DataAnnotations;

namespace MovieCaseDev.Entities.Dtos
{
    public class AddRatingRequest
    {
        public int MovieId { get; set; }
        public string? Note { get; set; }

        [Range(1, 10)]
        public int Score { get; set; }
    }
}

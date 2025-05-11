namespace MovieCaseDev.Entities.Dtos
{
    public class MovieDetailResponse
    {
        public int Id { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageScore { get; set; }
        public int TotalRatings { get; set; }
        public List<PublicRatingDto>? AllRatings { get; set; }
    }
    public class PublicRatingDto
    {
        public string UserEmail { get; set; }
        public int Score { get; set; }
        public string? Note { get; set; }
    }
   
}

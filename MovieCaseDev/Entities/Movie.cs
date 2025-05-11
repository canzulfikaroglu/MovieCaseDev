namespace MovieCaseDev.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public int ApiId { get; set; }
        public string OriginalTitle { get; set; } //film adı
        public string Overview { get; set; } //aciklama
        public DateTime ReleaseDate { get; set; } //cikiş tarihi
        public List<MovieRating> Ratings { get; set; }
    }
}

namespace MovieCaseDev.Entities.Dtos
{
    public class MovieApi
    {
         
        public int Page { get; set; }
        public List<ApiMovie> Results { get; set; }
    }
    public class ApiMovie
    {
        public int Id { get; set; }
        public string Original_Title { get; set; }
        public string Overview { get; set; }
        public DateTime Release_Date { get; set; }
    }
}


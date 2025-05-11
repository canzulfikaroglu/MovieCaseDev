using MovieCaseDev.Data;
using MovieCaseDev.Entities.Dtos;
using MovieCaseDev.Entities;
using RestSharp;
using System.Text.Json;
using System.Security.Claims;



namespace MovieCaseDev.Services.Concrete
{
    public class MovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;


        }


        public async Task ApiGet()
        {
            for (int page = 1; 20 >= page; page++)
            {
                var options = new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page={page}&sort_by=popularity.desc");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZTYzOTk1MzIzNTc4MTgyNmRjNjNiNDllYWU3MDExMCIsIm5iZiI6MS43NDY0NzY3OTUwNjA5OTk5ZSs5LCJzdWIiOiI2ODE5MWVmYjc1ZGE3YWY3MWEwOGE3NDQiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.kmBgjXKvE76MNFkPwyg340ZSshW9DPbcKrB9Fp0XqZ4");
                var response = await client.GetAsync(request);




                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content;
                    var apiData = JsonSerializer.Deserialize<MovieApi>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (apiData?.Results != null)
                    {
                        foreach (var apiMovie in apiData.Results)
                        {
                            if (!_context.Movies.Any(m => m.ApiId == apiMovie.Id))
                            {
                                var movie = new Movie
                                {
                                    ApiId = apiMovie.Id,
                                    OriginalTitle = apiMovie.Original_Title,
                                    Overview = apiMovie.Overview,
                                    ReleaseDate = DateTime.SpecifyKind(apiMovie.Release_Date, DateTimeKind.Utc),
                                    ApiPageNumber = page

                                };

                                _context.Movies.Add(movie); //bunu açtığımızda veritabanına kaydediyor 
                                Console.WriteLine($"ApiId: {movie.ApiId}, Title: {movie.OriginalTitle}, Overview: {movie.Overview}, ReleaseDate: {movie.ReleaseDate}");
                                Console.WriteLine();
                               
                            }
                        }
                        await _context.SaveChangesAsync();


                        Console.WriteLine("Film verileri API'den çekildi!");
                        Console.WriteLine("Veriler başarıyla kaydedildi.");
                    
                    }
                }
                else
                {
                    Console.WriteLine("API'den veri alınamadı.");
                }
            }

        }


    }
}

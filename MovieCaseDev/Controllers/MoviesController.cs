using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCaseDev.Data;
using MovieCaseDev.Entities;
using MovieCaseDev.Entities.Dtos;
using MovieCaseDev.Services;
using System.Security.Claims;

namespace MovieCaseDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;
        private readonly AppDbContext _context;

        public MoviesController(MovieService movieService,AppDbContext context)
        {
            _movieService = movieService;
            _context = context;
        }
        [Authorize]
        [HttpPost("load-from-api")]
        public async Task<IActionResult> LoadMoviesFromApi()
        {
            await _movieService.ApiGet();
            return Ok("Filmler başarıyla yüklendi.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than zero.");
            }

            var movies = await _context.Movies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(movies);
        }
        [Authorize]
        [HttpPost("{movieId}/rate")]
        public async Task<IActionResult> AddRating(int movieId,[FromBody] AddRatingRequest request)
        {
            var userEmail = User?.FindFirst(ClaimTypes.Email)?.Value;

            userEmail = "example@gmail.com";
            //if (userEmail == null)
            //    return Unauthorized("Kullanıcı doğrulanamadı.");

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.ApiId == movieId);
            if (movie == null)
                return NotFound("Film bulunamadı.");
            // ApiIdyi aktaramadım çünkü foreign key olduğu için idye göre yapmam gerekiyordu
            var rating = new MovieRating
            {
                MovieId = movie.Id,
                UserEmail = userEmail,
                Note = request.Note,
                Score = request.Score
            };

            _context.MovieRatings.Add(rating);
            await _context.SaveChangesAsync();

            return Ok("Puan eklendi.");
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            // Film bilgilerini al
            var movie = await _context.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Ratings)  // Yorumları da dahil et
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound("Film bulunamadı.");
            }


            var averageScore = movie.Ratings.Any()
                ? movie.Ratings.Average(r => r.Score)
                : 0;


            var totalRatings = movie.Ratings.Count();


            var allRatings = movie.Ratings.Select(r => new PublicRatingDto
            {
                UserEmail = r.UserEmail,
                Score = r.Score,
                Note = r.Note
            }).ToList();

   
            var movieDetailResponse = new MovieDetailResponse
            {
                Id = movie.Id,
                OriginalTitle = movie.OriginalTitle,
                Overview = movie.Overview,
                ReleaseDate = movie.ReleaseDate,
                AverageScore = averageScore,
                TotalRatings = totalRatings,
                AllRatings = allRatings
            };

            return Ok(movieDetailResponse);
        }


    }
}

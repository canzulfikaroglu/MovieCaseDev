using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCaseDev.Data;
using MovieCaseDev.Services.Abstract;

namespace MovieCaseDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEMailService _emailService;
        private readonly AppDbContext _context;
        public EmailController(IEMailService emailService,AppDbContext context)
        {
            _emailService = emailService;
            _context = context;
        }
        [HttpPost("{id}/recommend")]
        public async Task<IActionResult> RecommendMovie(int id,[FromBody] EmailRequest request)
        {
            var film = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            string subject = "Bugünün Film Tavsiyesi";
            string body = $"Merhaba! Bugünün film tavsiyesi: <strong>{film.OriginalTitle}</strong>. İyi seyirler ";
            await _emailService.SendEmailAsync(request.Email, subject, body);
            return Ok("Email gönderildi.");
        }
    }
    public class EmailRequest
    {
        public string Email { get; set; }
    }
}

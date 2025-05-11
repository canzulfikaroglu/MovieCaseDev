using MovieCaseDev.Services.Concrete;

namespace MovieCaseDev.Services
{
    public class MovieSyncBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromHours(1);
        public MovieSyncBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var movieService = scope.ServiceProvider.GetRequiredService<MovieService>();
                    try
                    {
                        Console.WriteLine($"[{DateTime.Now}] Movie sync started.");
                        await movieService.ApiGet();
                        Console.WriteLine($"[{DateTime.Now}] Movie sync completed.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[{DateTime.Now}] Error during movie sync: {ex.Message}");
                    }
                }
                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context ;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(g => g.Genre)
                .OrderByDescending(g => g.Rate)
                .ToListAsync();
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;

        }

        public Movie DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }
        public async Task<bool> isValidGenre(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public Movie UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }

        Task IMoviesService.AddMovieAsync(Movie movie)
        {
            return AddMovieAsync(movie);
        }
    }
}

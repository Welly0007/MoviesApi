using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Movie UpdateMovie(Movie movie);
        Task<bool> isValidGenre(int id);
        Movie DeleteMovie(Movie movie);
    }
}

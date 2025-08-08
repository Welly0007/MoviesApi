using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Services;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IMoviesService moviesService) : Controller
    {
        private readonly IMoviesService _moviesService = moviesService;

        private List<string> _extensionAllowed = [".jpg", ".png"];
        private int _maxSize = 1048576;


        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var movies = await _moviesService.GetAllMoviesAsync();
            if (movies == null)
                return NotFound("No Movies Found");

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            //movie.Genre = await _context.Genres.FindAsync(movie.GenreId);

            if (movie == null)
                return NotFound($"Movie with Id {id} not found");
            return Ok(movie);
        }
        [HttpPost]
        public async Task<IActionResult> addMovie([FromForm] MovieDto dto)
        {
            if (!_extensionAllowed.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only jpg and png extensions are Allowed");


            if (dto.Poster.Length > _maxSize)
                return BadRequest("Max file Size is 1 MB");

            var isValidGenre = await _moviesService.isValidGenre(dto.GenreId);

            if (!isValidGenre)
                return BadRequest("Genre Not found");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                StoryLine = dto.StoryLine,
                Poster = dataStream.ToArray(),
            };
            await _moviesService.AddMovieAsync(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateMovie(int id, [FromForm] MovieDto dto)
        {
            if (!_extensionAllowed.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only jpg and png extensions are Allowed");


            if (dto.Poster.Length > _maxSize)
                return BadRequest("Max file Size is 1 MB");

            var isValidGenre = await _moviesService.isValidGenre(dto.GenreId);

            if (!isValidGenre)
                return BadRequest("Genre Not found");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = await _moviesService.GetMovieByIdAsync(id);

            if (movie is null)
                return NotFound($"Movie with id {id} not found");
            movie.GenreId = dto.GenreId;
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.StoryLine = dto.StoryLine;
            movie.Poster = dataStream.ToArray();

            _moviesService.UpdateMovie(movie);

            return Ok(movie);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie is null)
                return NotFound($"Movie with id {id} not found");
            _moviesService.DeleteMovie(movie);
            return Ok(movie);
        }
    }
}

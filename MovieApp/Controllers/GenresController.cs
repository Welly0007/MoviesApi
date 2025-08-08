using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetGenresAsync()
        {
            var genres = await _context.Genres.OrderBy(g=>g.Name).ToListAsync();

            return Ok(genres);
        }

        [HttpPost]

        public async Task<IActionResult> CreateGenreAsync(string name)
        {
            Genre genre = new() { Name = name };
            await _context.AddAsync(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateGenre(byte id, [FromBody] string name)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre is null)
                return NotFound($"genre with id {id} not found");

            genre.Name = name;

            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGenre(byte id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre is null)
                return NotFound($"Genre with Id {id} not found");

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }
    }
}

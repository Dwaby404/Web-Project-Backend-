using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeApi.Models;

namespace AnimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeDbContext _context;

        public AnimeController(AnimeDbContext context)
        {
            _context = context;
        }

        // GET: api/animes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnime()
        {
            return await _context.Anime.ToListAsync();
        }

        // GET: api/animes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnime(int id)
        {
            var anime = await _context.Anime.FindAsync(id);

            if (anime == null)
            {
                return NotFound(new { message = "Anime not found" });
            }

            return anime;
        }

        // GET: api/animes/genre/Action - Filtered query example
        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnimesByGenre(string genre)
        {
            var anime = await _context.Anime
                .Where(a => a.Genre == genre)
                .ToListAsync();

            return anime;
        }

        // GET: api/animes/year/2023 - Another filtered query
        [HttpGet("year/{year}")]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnimesByYear(int year)
        {
            var anime = await _context.Anime
                .Where(a => a.ReleaseYear == year)
                .ToListAsync();

            return anime;
        }

        // POST: api/animes
        [HttpPost]
        public async Task<ActionResult<Anime>> CreateAnime(Anime anime)
        {
            // Duplicate code check
            if (await _context.Anime.AnyAsync(a => a.Code == anime.Code))
            {
                return BadRequest(new { message = "Anime code already exists" });
            }

            _context.Anime.Add(anime);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnime), new { id = anime.Id }, anime);
        }

        // PUT: api/animes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, Anime anime)
        {
            if (id != anime.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            // Check if code is taken by another anime
            if (await _context.Anime.AnyAsync(a => a.Code == anime.Code && a.Id != id))
            {
                return BadRequest(new { message = "Anime code already exists" });
            }

            _context.Entry(anime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AnimeExists(id))
                {
                    return NotFound(new { message = "Anime not found" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/animes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var anime = await _context.Anime.FindAsync(id);
            if (anime == null)
            {
                return NotFound(new { message = "Anime not found" });
            }

            _context.Anime.Remove(anime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AnimeExists(int id)
        {
            return await _context.Anime.AnyAsync(e => e.Id == id);
        }
    }
}
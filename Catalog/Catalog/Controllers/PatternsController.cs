using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatternsController : ControllerBase
    {
        private readonly TodoContext _context;

        public PatternsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Patterns
        // Добавить Patterns из database
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<Pattern> GetPatterns()
        {
            var patterns = Startup.database.getpat();
            foreach (var pattern in patterns)
            {
                _context.db_patterns.Add(pattern);
            }
           _context.SaveChanges();


            var categories = Startup.database.getcat();
            foreach (var category in categories)
            {
                _context.db_categories.Add(category);
            }
            _context.SaveChanges();

            return _context.GetPatterns();
        }

        // GET: api/Patterns/All
        [HttpGet("All")]
        [Authorize]
        public IEnumerable<Pattern> GetPatternsAll()
        {
            return _context.GetPatterns();
        }

        // GET: api/Patterns
        // Вывести все выкройки по уровню сложности
        [HttpGet("GetPatternsByLevel")]
        [Authorize]
        public IEnumerable<Pattern> GetPatternsByLevel(Pattern p)
        {
            return _context.GetPatternByLevel(p.NameLevel);
        }

        [HttpGet("GetPatternsByCategory")]
        [Authorize]
        public IEnumerable<Pattern> GetPatternsByCategory(Category c)
        {
            return _context.GetPatternByCategory(c.NameCategory);
        }

        // GET: api/Patterns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pattern>> GetPattern(int id)
        {
            var pattern = await _context.db_patterns.FindAsync(id);
            if (pattern == null)
            {
                return NotFound();
            }
            return pattern;
        }

        // PUT: api/Patterns/5 
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutPattern(int id, Pattern pattern)
        {
            if (id != pattern.Id)
            {
                return BadRequest();
            }
            _context.ChangePrice(id, pattern.Price); ////
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatternExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        // POST: api/Patterns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Pattern>> PostPattern(Pattern pattern)
        {
            if (pattern.Id != 0) return null;
            _context.db_patterns.Add(pattern);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPattern", new { id = pattern.Id }, pattern);
        }

        // DELETE: api/Patterns/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Pattern>> DeletePattern(int id)
        {
            var pattern = await _context.db_patterns.FindAsync(id);
            if (pattern == null)
            {
                return NotFound();
            }

            _context.db_patterns.Remove(pattern);
            await _context.SaveChangesAsync();

            return pattern;
        }

        private bool PatternExists(int id)
        {
            return _context.db_patterns.Any(e => e.Id == id);
        }
    }
}

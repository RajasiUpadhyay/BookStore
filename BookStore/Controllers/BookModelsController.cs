using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookModelsController : ControllerBase
    {
        private readonly BooksContext _context;

        public BookModelsController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/BookModels
        [HttpGet]
        public IEnumerable<BookModel> Getbooks()
        {
            if (!_context.books.Any())
            {
                var folderDetails = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"data\\books.json"}");
                var JSON = System.IO.File.ReadAllText(folderDetails);
                dynamic jsonObj = JsonConvert.DeserializeObject<BookModel[]>(JSON);
                foreach (BookModel obj in jsonObj)
                {
                    _context.books.Add(obj);
                    _context.SaveChanges();
                }
            }
            return _context.books;
        }

        // GET: api/BookModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookModel = await _context.books.FindAsync(id);

            if (bookModel == null)
            {
                return NotFound();
            }

            return Ok(bookModel);
        }

        // PUT: api/BookModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookModel([FromRoute] int id, [FromBody] BookModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookModels
        [HttpPost]
        public async Task<IActionResult> PostBookModel([FromBody] BookModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.books.Add(bookModel);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetBookModel", new { id = bookModel.Id }, bookModel);
            return CreatedAtAction(nameof(GetBookModel), new { id = bookModel.Id }, bookModel);
        }

        // DELETE: api/BookModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookModel = await _context.books.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }

            _context.books.Remove(bookModel);
            await _context.SaveChangesAsync();

            return Ok(bookModel);
        }

        private bool BookModelExists(int id)
        {
            return _context.books.Any(e => e.Id == id);
        }
    }
}
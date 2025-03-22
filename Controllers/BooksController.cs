using _6_modul_exam.Entities;
using _6_modul_exam.Services;
using _6_modul_exam.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _6_modul_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IOptions<BookApiSettings> _settings;
        private readonly HttpContext _context;

        public BooksController(
            IBookService bookService,
            IOptions<BookApiSettings> settings)
        {
            _bookService = bookService;
            _settings = settings;
        }

        // GetAll
        [HttpGet]
        public IActionResult GetAll([FromQuery]int? year)
        {
            HttpContext.Response.Headers.Add("X-Currency", _settings.Value.DefaultCurrency);

            if (year.HasValue)
            {
                var filteredBooks = _bookService.GetBooks()
                    .Where(b => b.PublishedYear.Year == year);

                return Ok(filteredBooks);
            }
            else
            {
                var books = _bookService.GetBooks();
                
                return Ok(books);
            }
            
        }

        // GetById
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            return Ok(_bookService.GetBookById(id));
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            if (_settings.Value.MaxAllowedBooks > _bookService.GetBooks().Count)
            {
                var validator = new BookValidator();

                var result = await validator.ValidateAsync(bookDto);

                if (!result.IsValid)
                {
                    return BadRequest();
                }

                await _bookService.AddBookAsync(bookDto);

                await _bookService.SaveChangesAsync();

                return Created();
            }

            return BadRequest("Reached book limit! Remove one first!");
        }

        // Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDto bookDto)
        {
            await _bookService.Update(bookDto, id);

            await _bookService.SaveChangesAsync();

            return Ok();
        }

        // Delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _bookService.Delete(id);

            await _bookService.SaveChangesAsync();

            return Ok();
        }
    }
}

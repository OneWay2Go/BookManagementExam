using _6_modul_exam.Entities;
using _6_modul_exam.Validators;
using Microsoft.AspNetCore.Http.HttpResults;

namespace _6_modul_exam.Services
{
    public class BookService : IBookService
    {
        private readonly BookContext _context;

        public BookService(BookContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(BookDto book)
        {
            var bookOriginal = new Book()
            {
                Author = book.Author,
                Price = book.Price,
                PublishedYear = book.PublishedYear,
                Title = book.Title,
            };

            await _context.AddAsync(bookOriginal);
        }

        public async Task Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            _context.Books.Remove(book);
        }

        public BookDto GetBookById(int id)
        {
            var book = _context.Books.FindAsync(id).Result;

            if (book != null)
            {
                var bookDto = new BookDto()
                {
                    Author = book.Author,
                    Price = book.Price,
                    PublishedYear = book.PublishedYear,
                    Title = book.Title
                };

                return bookDto;
            }

            throw new Exception($"There is no book with {id} id.");
        }

        public IList<Book> GetBooks()
        {
            var books = _context.Books.ToList();

            return books;
        }

        public async Task Update(BookDto bookDto, int id)
        {
            if (bookDto != null)
            {
                var validator = new BookValidator();

                var result = await validator.ValidateAsync(bookDto);

                if (!result.IsValid)
                {
                    throw new Exception("Book could not pass the validator!");
                }

                var book = new Book()
                {
                    Id = id,
                    Author = bookDto.Author,
                    Price = bookDto.Price,
                    PublishedYear = bookDto.PublishedYear,
                    Title = bookDto.Title
                };

                _context.Books.Update(book);
            }
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}

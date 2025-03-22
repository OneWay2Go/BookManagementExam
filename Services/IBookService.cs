using _6_modul_exam.Entities;

namespace _6_modul_exam.Services
{
    public interface IBookService
    {
        IList<Book> GetBooks();

        BookDto GetBookById(int id);

        Task AddBookAsync(BookDto book);

        Task Update(BookDto bookDto, int id);

        Task Delete(int id);

        Task<int> SaveChangesAsync();
    }
}

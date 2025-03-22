namespace _6_modul_exam.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public decimal Price { get; set; }

    public DateOnly PublishedYear { get; set; }
}

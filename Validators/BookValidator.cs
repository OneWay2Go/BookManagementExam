using _6_modul_exam.Entities;
using FluentValidation;

namespace _6_modul_exam.Validators
{
    public class BookValidator : AbstractValidator<BookDto>
    {
        public BookValidator()
        {
            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("Author section have to be filled!");

            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title section have to be filled!");

            RuleFor(b => b.Price)
                .NotEmpty().WithMessage("Price section have to be filled!")
                .GreaterThan(0).WithMessage("Price section have to be greater than 0!");

            RuleFor(b => b.PublishedYear)
                .NotEmpty().WithMessage("Year section have to be filled!")
                .Must(b => b.Year >= 1900 && b.Year <= 2025).WithMessage("Year section have to be between 1900 and 2025!");
        }
    }
}

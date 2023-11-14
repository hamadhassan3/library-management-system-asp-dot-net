using FluentValidation;
using HH.Lms.Service.Library.Dto;

namespace HH.Lms.Service.Library.Validator;

public class BookDtoValidator : AbstractValidator<BookDto>
{
    public BookDtoValidator()
    {
        RuleFor(dto => dto.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(dto => dto.Isbn).NotEmpty().WithMessage("ISBN is required");
        RuleFor(dto => dto.Author).NotEmpty().WithMessage("Author is required");
    }
}

using FluentValidation;
using HH.Lms.Service.Library.Dto;

namespace HH.Lms.Service.Library.Validator;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(dto => dto.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(dto => dto.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(dto => dto.Role)
            .Must(role => role == "User" || role == "Admin")
            .WithMessage("Role must be either 'User' or 'Admin'");
    }
}

using Core.Dtos;
using FluentValidation;

namespace Core.Validators
{
    public class RegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Gender).IsInEnum();
        }
    }
}

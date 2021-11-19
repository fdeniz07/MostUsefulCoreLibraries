using FluentValidation;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.FluentValidators
{
    public class AddressValidator:AbstractValidator<Address>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alani bos olamaz";

        public AddressValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(x => x.Province).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(x => x.PostCode).NotEmpty().WithMessage(NotEmptyMessage).MaximumLength(5)
                .WithMessage("{PropertyName} alani  en fazla {MaxLenght} karakter olmalidir.");
        }
    }
}

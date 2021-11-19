using System;
using FluentValidation;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alani bos olamaz";
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Email).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress()
                .WithMessage("Email alani dogru formatta olmalidir");
            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(18, 60)
                .WithMessage("Age alani 18 ile 60 arasinda olmalidir.");

            //Custom Validatorlar must metodu ile kullanilir. Ortak alanlarbir class olarak yazilip,ilgili validatorlarda cagrilabilir
            //Custom yazilan kurallar, server'e post edilmeden devreye girmez. Bu nedenle, ayni kurallar javascript jquery kütüphanesi kullanilarak ajax islemleri yapilmalidir. Eger gönderilen data cok büyük degil ise ekstra kod yazmamiza yok.

            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>
            {
                return DateTime.Now.AddYears(-18) >= x;
            }).WithMessage("Yasiniz 18 yasindan büyük olmalidir");

            RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());

            RuleFor(x=>x.Gender).IsInEnum().WithMessage("{ PropertyName} alani Erkek=1, Bayan=2 olmalidir.").NotEmpty().WithMessage(NotEmptyMessage);

        }
    }
}

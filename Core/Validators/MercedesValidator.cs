using FluentValidation;
using Core.Dtos;
using Data.Entities;

namespace Core.Validators
{
    public class MercedesValidator : AbstractValidator<MercedesDto>
    {
        public MercedesValidator()
        {
            RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Price)
                .GreaterThan(0);
            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100);
            RuleFor(x => x.HorsePower)
               .GreaterThan(0);
            RuleFor(x => x.Year)
               .GreaterThan(1900);
            RuleFor(x => x.Class)
               .NotNull()
               .NotEmpty()
               .WithMessage("Class is required!");
            RuleFor(x => x.Volume)
                .GreaterThan(0);
            RuleFor(x => x.BrandOfCarId)
                .GreaterThan(0).WithMessage("Brand is required.");
        }

        public bool ValidateUri(string? uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                return true;
            }
            return Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}

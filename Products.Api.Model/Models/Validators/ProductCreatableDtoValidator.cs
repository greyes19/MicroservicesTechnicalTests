using FluentValidation;

namespace Products.Api.Services.Models.Validators
{
    public class ProductCreatableDtoValidator : AbstractValidator<ProductCreatableDto>
    {
        public ProductCreatableDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MaximumLength(250);
            RuleFor(m => m.LotNumber).NotEmpty();
            RuleFor(m => m.Cost).NotEmpty();
            RuleFor(m => m.SalePrice).NotEmpty();
        }
    }
}

using FluentValidation;

namespace Products.Domain.Model.Models.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MaximumLength(250);
            RuleFor(m => m.LotNumber).NotEmpty();
            RuleFor(m => m.Cost).NotEmpty();
            RuleFor(m => m.SalePrice).NotEmpty();
        }
    }
}

using FluentValidation;

namespace Purchase.Api.Model.Models.Validators
{
    public class PurchaseDetailCreatableDtoValidator : AbstractValidator<PurchaseDetailCreatableDto>
    {
        public PurchaseDetailCreatableDtoValidator()
        {
            RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El producto no puede ser nulo.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

        }
    }
}

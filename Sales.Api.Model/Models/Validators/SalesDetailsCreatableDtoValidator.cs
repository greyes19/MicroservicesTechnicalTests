using FluentValidation;

namespace Sales.Api.Model.Models.Validators
{
    public class SalesDetailsCreatableDtoValidator : AbstractValidator<SalesDetailsCreatableDto>
    {
        public SalesDetailsCreatableDtoValidator()
        {
            RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El producto Id no puede ser nulo.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
        }
    }
}

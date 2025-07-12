using FluentValidation;

namespace Sales.Api.Model.Models.Validators
{
    public class SalesCreatableDtoValidator : AbstractValidator<SalesCreatableDto>
    {
        public SalesCreatableDtoValidator()
        {
            RuleFor(m => m.Details).NotEmpty();
            RuleForEach(m => m.Details)
            .SetValidator(new SalesDetailsCreatableDtoValidator());
        }
    }
}

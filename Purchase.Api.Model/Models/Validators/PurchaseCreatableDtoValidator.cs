using FluentValidation;

namespace Purchase.Api.Model.Models.Validators
{
    public class PurchaseCreatableDtoValidator : AbstractValidator<PurchaseCreatableDto>
    {
        public PurchaseCreatableDtoValidator()
        {
            RuleFor(m => m.PurchaseDetails).NotEmpty();
            RuleForEach(m => m.PurchaseDetails)
            .SetValidator(new PurchaseDetailCreatableDtoValidator());
        }
    }
}

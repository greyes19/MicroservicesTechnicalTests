using FluentValidation;

namespace Movement.Api.Model.Models.Validators
{
    public class MovementCreatableDtoValidator : AbstractValidator<MovementCreatableDto>
    {
        public MovementCreatableDtoValidator()
        {
            RuleFor(m => m.MovementType).NotNull().IsInEnum();
            RuleFor(m => m.OriginDocumentId).NotEmpty();
            RuleFor(m => m.Details).NotEmpty();
        }
    }
}

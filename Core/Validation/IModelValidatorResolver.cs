using FluentValidation;

namespace Core.Validation
{
    public interface IModelValidatorResolver
    {
        IValidator<T> GetValidator<T>() where T : class;
        IValidator<T> GetValidator<T>(T entity) where T : class;
        IValidator GetValidator(Type type);
    }
}

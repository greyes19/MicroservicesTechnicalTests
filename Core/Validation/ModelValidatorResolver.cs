using Microsoft.Extensions.DependencyInjection;

namespace Core.Validation
{
    public class ModelValidatorResolver : IModelValidatorResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ModelValidatorResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public FluentValidation.IValidator<T> GetValidator<T>() where T : class
        {
            return _serviceProvider.GetService<FluentValidation.IValidator<T>>();
        }

        public FluentValidation.IValidator GetValidator(Type modelType)
        {
            Type validatorType = typeof(FluentValidation.IValidator<>);
            Type[] typeArgs = { modelType };
            Type modelValidatorType = validatorType.MakeGenericType(typeArgs);

            return (FluentValidation.IValidator)_serviceProvider.GetService(modelValidatorType);
        }

        public FluentValidation.IValidator<T> GetValidator<T>(T entity) where T : class
        {

            return _serviceProvider.GetService<FluentValidation.IValidator<T>>();
        }
    }
}

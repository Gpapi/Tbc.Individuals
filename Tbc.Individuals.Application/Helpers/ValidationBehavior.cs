using FluentValidation;
using MediatR;

namespace Tbc.Individuals.Application.Helpers;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
               where TRequest : IBaseRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var tasks = validators
                .Select(async v => await v.ValidateAsync(context));

            var failures = (await Task.WhenAll(tasks))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .Select(x => x.ErrorMessage)
            .ToArray();

            if (failures.Length != 0)
            {
                throw new ApplicationValidationException(failures);

            }
        }
        return await next(cancellationToken);
    }
}

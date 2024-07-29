using Application.Commons.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons.Behaviours;
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                 validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.Where(x => x.Errors.Any()).SelectMany(x => x.Errors).ToList();
            /* var errorDictionnary = validators.Select(x => x.Validate(context))
                                     .SelectMany(x => x.Errors)
                                     .Where(x => x is not null)
                                     .GroupBy(
                                      x => x.PropertyName.Substring(x.PropertyName.IndexOf(".") + 1),
                                      x=> x.ErrorMessage, (propertyName, errorMessage) => new
                                      {
                                          Key = propertyName,
                                          Values = errorMessage.Distinct().ToArray()
                                      }).ToDictionary(x=>x.Key,x=>x.Values);*/
            if (failures.Any()) throw new ValidationException(failures);
        }
        return await next();
    }
}

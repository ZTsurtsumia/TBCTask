using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonDirectory.Application.Dtos;
using PersonDirectory.Application.Exceptions;
using ValidationException = PersonDirectory.Application.Exceptions.ValidationException;

namespace PersonDirectory.Api.Filters
{
    public class ValidateModelStateActionFilter(IValidator<AddPersonRequest> validator) : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.ActionArguments.Values
                            .FirstOrDefault(a => a is AddPersonRequest) is AddPersonRequest model)
            {
                var validationResult = validator.Validate(model);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(
                        e => new ValidationError(e.PropertyName, e.ErrorMessage)).ToList();

                    throw new ValidationException(errors);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
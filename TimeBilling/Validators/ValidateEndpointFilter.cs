﻿
using FluentValidation;
using TimeBilling.Models;

namespace TimeBilling.Validators;

public class ValidateEndpointFilter<T> : IEndpointFilter
    where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();

        var model = context.Arguments
        .OfType<T>()
        .FirstOrDefault();

        if (model is not null)
        {

            var validation = validator.Validate(model);

            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
        }
        return await next(context);
    }
}

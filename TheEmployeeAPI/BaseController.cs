using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace TheEmployeeAPI;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : Controller
{
        protected async Task<ValidationResult> ValidateAsync<T>(T instance)
    {
        var validator = HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator == null)
        {
            throw new ArgumentException($"No validator found for {typeof(T).Name}");
        }
        var validationContext = new ValidationContext<T>(instance);

        return await validator.ValidateAsync(validationContext);
    }   

}
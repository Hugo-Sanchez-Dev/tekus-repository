#region Usings
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using Tekus.Providers.Application.DTOs.Response;
using Tekus.Providers.Application.Enum;
#endregion

namespace Tekus.Providers.API.Handler;

public class CustomValidationResponseFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        Dictionary<string, string[]> errors = context.ModelState
            .Where(ms => ms.Value.Errors.Any())
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        ResponseDTO<object> response = new ResponseDTO<object>()
        {
            Header = new HeaderDTO
            {
                ResponseCode = ResponseCodeEnum.BAD_REQUEST,
                Message = "Data validation errors."
            },
            Data = errors
        };

        return new BadRequestObjectResult(response);
    }
}

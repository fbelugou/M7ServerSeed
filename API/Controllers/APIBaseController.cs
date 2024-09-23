using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// This class is the base class for all the API controllers in the application.
/// </summary>
[Authorize(Roles = "USER")]
[ApiController] // This attribute is used to indicate that the class is an API controller.
[Route("api/[Controller]")] // This attribute is used to specify the route template for the controller [Controller]
                            // is replaced by the name of the controller class.
public abstract class APIBaseController : ControllerBase
{

    protected async Task<BadRequestObjectResult> ValidateRequest<R, V>(R request, V validator) where V : AbstractValidator<R> 
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            ValidationProblemDetails problemDetails = new(validationResult.ToDictionary())
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1" //RFC link 400 Bad Request
            };

            return BadRequest(problemDetails); // 400 Bad Request
        }
        return null;
    }
}

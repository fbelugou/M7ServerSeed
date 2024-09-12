using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// This class is the base class for all the API controllers in the application.
/// </summary>
[ApiController] // This attribute is used to indicate that the class is an API controller.
[Route("api/[Controller]")] // This attribute is used to specify the route template for the controller [Controller]
                            // is replaced by the name of the controller class.
public abstract class APIBaseController : ControllerBase
{
}

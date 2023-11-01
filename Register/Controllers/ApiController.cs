using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Register.Controllers;

[ApiController]
[Route("[controller]")]

public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        }; 
    }
}
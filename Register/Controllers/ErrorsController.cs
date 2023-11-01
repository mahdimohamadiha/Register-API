using Microsoft.AspNetCore.Mvc;

namespace Register.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
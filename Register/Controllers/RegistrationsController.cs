using Microsoft.AspNetCore.Mvc;
using Register.Contracts.Registration;
using Register.Models;

namespace Register.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    [HttpPost()]
    public IActionResult CreateRegistration(CreateRegistrationRequest request)
    {
        var Registration = new Registration(
            Guid.NewGuid(),
            request.UserName,
            request.Password,
            request.FirstName,
            request.LastName,
            request.City,
            request.PhoneNumber,
            request.BirthDate,
            DateTime.UtcNow
        );

        var response = new RegistrationResponse(
            Registration.Id,
            Registration.UserName,
            Registration.Password,
            Registration.FirstName,
            Registration.LastName,
            Registration.City,
            Registration.PhoneNumber,
            Registration.BirthDate,
            Registration.RegisterationDateTime
        );

        return CreatedAtAction(
            actionName: nameof(GetRegistration),
            routeValues: new {id = Registration.Id},   
            value: response);
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetRegistration(Guid id)
    {
        return Ok(id);
    }

    [HttpPut("{id:Guid}")]
    public IActionResult UpsertRegistration(Guid id ,UpsertRegistrationRequest request)
    {
        return Ok(request);
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeleteRegistration(Guid id)
    {
        return Ok(id);
    }
}
using Microsoft.AspNetCore.Mvc;
using Register.Contracts.Registration;
using Register.Models;
using Register.Services.Registrations;

namespace Register.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationsService _registrationsService;

    public RegistrationController(IRegistrationsService registrationsService)
    {
        _registrationsService = registrationsService;
    }

    [HttpPost]
    public IActionResult CreateRegistration(CreateRegistrationRequest request)
    {
        var registration = new Registration(
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

        _registrationsService.CreateRegistration(registration);

        var response = new RegistrationResponse(
            registration.Id,
            registration.UserName,
            registration.Password,
            registration.FirstName,
            registration.LastName,
            registration.City,
            registration.PhoneNumber,
            registration.BirthDate,
            registration.RegisterationDateTime
        );

        return CreatedAtAction(
            actionName: nameof(GetRegistration),
            routeValues: new {id = registration.Id},   
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
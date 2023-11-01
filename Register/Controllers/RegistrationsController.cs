using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Register.Contracts.Registration;
using Register.Models;
using Register.ServiceErrors;
using Register.Services.Registrations;

namespace Register.Controllers;

public class RegistrationController : ApiController
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
        ErrorOr<Registration> getRegistrationResult = _registrationsService.GetRegistration(id);

        return getRegistrationResult.Match(
            registration => Ok(MapRegistrationResponse(registration)),
            errors => Problem()
        );

        // if (getRegistrationResult.IsError && getRegistrationResult.FirstError == Errors.Registration.NotFound)
        // {
        //     return NotFound();
        // }

        // var registration = getRegistrationResult.Value;
        // RegistrationResponse response = MapRegistrationResponse(registration);
        // return Ok(response);
    }

    private static RegistrationResponse MapRegistrationResponse(Registration registration)
    {
        return new RegistrationResponse(
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
    }

    [HttpPut("{id:Guid}")]
    public IActionResult UpsertRegistration(Guid id ,UpsertRegistrationRequest request)
    {
        var registration = new Registration(
            id,
            request.UserName,
            request.Password,
            request.FirstName,
            request.LastName,
            request.City,
            request.PhoneNumber,
            request.BirthDate,
            DateTime.UtcNow
        );
        _registrationsService.UpsertRegistration(registration);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeleteRegistration(Guid id)
    {
        _registrationsService.DeleteRegistration(id);
        return NoContent();
    }
}
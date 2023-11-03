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
        ErrorOr<Registration> requestToRegistrationResult = Registration.From(request);

        if (requestToRegistrationResult.IsError)
        {
            return Problem(requestToRegistrationResult.Errors);
        }

        var registration = requestToRegistrationResult.Value;

        ErrorOr<Created> createRegistrationResult = _registrationsService.CreateRegistration(registration);

        return createRegistrationResult.Match(
            created => CreatedAtGetRegistration(registration),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetRegistration(Guid id)
    {
        ErrorOr<Registration> getRegistrationResult = _registrationsService.GetRegistration(id);

        return getRegistrationResult.Match(
            registration => Ok(MapRegistrationResponse(registration)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:Guid}")]
    public IActionResult UpsertRegistration(Guid id ,UpsertRegistrationRequest request)
    {
        var requestToRegistrationResult = Registration.From(id, request);
        if (requestToRegistrationResult.IsError)
        {
            return Problem(requestToRegistrationResult.Errors);
        }
        var registration = requestToRegistrationResult.Value;
        ErrorOr<UpsertedRegistration> upsertedRegistrationResult = _registrationsService.UpsertRegistration(registration);

        return upsertedRegistrationResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetRegistration(registration) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeleteRegistration(Guid id)
    {
        ErrorOr<Deleted> deleteRegistrationResult = _registrationsService.DeleteRegistration(id);

        return deleteRegistrationResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
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

    private IActionResult CreatedAtGetRegistration(Registration registration)
    {
        return CreatedAtAction(
            actionName: nameof(GetRegistration),
            routeValues: new { id = registration.Id },
            value: MapRegistrationResponse(registration));
    }
}
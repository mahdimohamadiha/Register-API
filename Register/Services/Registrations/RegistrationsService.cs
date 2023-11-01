using ErrorOr;
using Register.Models;
using Register.ServiceErrors;

namespace Register.Services.Registrations;

public class RegistrationsService : IRegistrationsService
{
    private static readonly Dictionary<Guid, Registration> _registrations = new();

    public void CreateRegistration(Registration register)
    {
        _registrations.Add(register.Id, register);
    }

    public void DeleteRegistration(Guid id)
    {
         _registrations.Remove(id);
    }

    public ErrorOr<Registration> GetRegistration(Guid id)
    {
        if (_registrations.TryGetValue(id, out var registration))
        {
            return registration;
        }
        return Errors.Registration.NotFound;
    }

    public void UpsertRegistration(Registration registration)
    {
        _registrations[registration.Id] = registration;
    }
}
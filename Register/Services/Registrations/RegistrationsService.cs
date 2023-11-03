using ErrorOr;
using Register.Models;
using Register.ServiceErrors;

namespace Register.Services.Registrations;

public class RegistrationsService : IRegistrationsService
{
    private static readonly Dictionary<Guid, Registration> _registrations = new();

    public ErrorOr<Created> CreateRegistration(Registration register)
    {
        _registrations.Add(register.Id, register);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteRegistration(Guid id)
    {
        _registrations.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Registration> GetRegistration(Guid id)
    {
        if (_registrations.TryGetValue(id, out var registration))
        {
            return registration;
        }
        return Errors.Registration.NotFound;
    }

    public ErrorOr<UpsertedRegistration> UpsertRegistration(Registration registration)
    {
        var IsNewlyCreated = !_registrations.ContainsKey(registration.Id);
        _registrations[registration.Id] = registration;

        return new UpsertedRegistration(IsNewlyCreated);
    }
}
using ErrorOr;
using Register.Contracts.Registration;
using Register.Models;

namespace Register.Services.Registrations;

public interface IRegistrationsService
{
    ErrorOr<Created> CreateRegistration(Registration register);
    ErrorOr<Deleted> DeleteRegistration(Guid id);
    ErrorOr<Registration> GetRegistration(Guid id);
    ErrorOr<UpsertedRegistration> UpsertRegistration(Registration registration);
}
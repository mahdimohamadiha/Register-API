using ErrorOr;
using Register.Contracts.Registration;
using Register.Models;

namespace Register.Services.Registrations;

public interface IRegistrationsService
{
    void CreateRegistration(Registration register);
    void DeleteRegistration(Guid id);
    ErrorOr<Registration> GetRegistration(Guid id);
    void UpsertRegistration(Registration registration);
}
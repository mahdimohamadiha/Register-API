using Register.Contracts.Registration;
using Register.Models;

namespace Register.Services.Registrations;

public interface IRegistrationsService
{
    void CreateRegistration(Registration register);
}
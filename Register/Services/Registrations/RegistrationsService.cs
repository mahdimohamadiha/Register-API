using Register.Models;

namespace Register.Services.Registrations;

public class RegistrationsService : IRegistrationsService
{
    private readonly Dictionary<Guid, Registration> _registrations = new();

    public void CreateRegistration(Registration register)
    {
        _registrations.Add(register.Id, register);
    }
}
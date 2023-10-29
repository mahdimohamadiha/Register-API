namespace Register.Contracts.Registration;

public record CreateRegistrationRequest(
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string City,
    string PhoneNumber,
    DateOnly BirthDate
);
namespace Register.Contracts.Registration;

public record UpsertRegistrationRequest(
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string City,
    string PhoneNumber,
    DateOnly BirthDate
);
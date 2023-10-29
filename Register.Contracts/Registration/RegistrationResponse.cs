namespace Register.Contracts.Registration;

public record RegistrationResponse(
    Guid Id,
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string City,
    string PhoneNumber,
    DateOnly BirthDate, 
    DateTime RegisterationDateTime
);
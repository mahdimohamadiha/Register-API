using ErrorOr;
using Register.Contracts.Registration;
using Register.ServiceErrors;

namespace Register.Models;

public class Registration
{
    public const int MinFirstNameLength = 3;
    public const int MaxFirstNameLength = 50;
    public const int MinLastNameLength = 3;
    public const int MaxLastNameLength = 50;

    public Guid Id { get; }
    public string UserName { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string City { get; }
    public string PhoneNumber { get; }
    public DateOnly BirthDate { get; }
    public DateTime RegisterationDateTime { get; }

    public Registration(
        Guid id,
        string userName,
        string password,
        string firstName,
        string lastName,
        string city,
        string phoneNumber,
        DateOnly birthDate,
        DateTime registerationDateTime)
    {
        Id = id;
        UserName = userName;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        City = city;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        RegisterationDateTime = registerationDateTime;
    }

    public static ErrorOr<Registration> Create(
        string userName,
        string password,
        string firstName,
        string lastName,
        string city,
        string phoneNumber,
        DateOnly birthDate,
        Guid? id = null
    )
    {
        List<Error> errors = new();

        if (firstName.Length is < MinFirstNameLength or > MaxFirstNameLength)
        {
            errors.Add(Errors.Registration.InvalidFirstName);
        }

        if (lastName.Length is < MinLastNameLength or > MaxLastNameLength)
        {
            errors.Add(Errors.Registration.InvalidLastName);
        }

        if (errors.Count > 0)
        {
            return errors;
        }
        return new Registration(
            id ?? Guid.NewGuid(),
            userName,
            password,
            firstName,
            lastName,
            city,
            phoneNumber,
            birthDate,
            DateTime.UtcNow
        );
    }

    public static ErrorOr<Registration> From(CreateRegistrationRequest request)
    {
        return Create(
            request.UserName,
            request.Password,
            request.FirstName,
            request.LastName,
            request.City,
            request.PhoneNumber,
            request.BirthDate
        );
    }

    public static ErrorOr<Registration> From(Guid id, UpsertRegistrationRequest request)
    {
        return Create(
            request.UserName,
            request.Password,
            request.FirstName,
            request.LastName,
            request.City,
            request.PhoneNumber,
            request.BirthDate,
            id
        );
    }
}
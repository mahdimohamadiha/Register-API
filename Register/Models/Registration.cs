namespace Register.Models;

public class Registration
{
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
}
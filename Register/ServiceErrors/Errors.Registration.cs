using ErrorOr;

namespace Register.ServiceErrors;

public static class Errors
{
    public static class Registration
    {
        public static Error InvalidLastName => Error.Validation(
            code: "Registration.InvalidLastName",
            description: $"Registration last name must be at least {Models.Registration.MinLastNameLength} characters long and at most {Models.Registration.MinLastNameLength} characters long."
        );
        public static Error InvalidFirstName => Error.Validation(
            code: "Registration.InvalidFirstName",
            description: $"Registration first name must be at least {Models.Registration.MinFirstNameLength} characters long and at most {Models.Registration.MinFirstNameLength} characters long."
        );
        public static Error NotFound => Error.NotFound(
            code: "Registration.NotFound",
            description: "Registration not found"
        );
    }
}
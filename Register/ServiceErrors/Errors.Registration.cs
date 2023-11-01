using ErrorOr;

namespace Register.ServiceErrors;

public static class Errors
{
    public static class Registration
    {
        public static Error NotFound => Error.NotFound(
            code: "Registration.NotFound",
            description: "Registration not found"
        );
    }
}
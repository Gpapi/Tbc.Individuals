namespace Tbc.Individuals.Application.Helpers;

public class ApplicationValidationException : Exception
{
    public ApplicationValidationException() : base()
    {
    }

    public ApplicationValidationException(string message) : base(message)
    {
        Errors = [message];
    }
    public ApplicationValidationException(string message, Exception innerException) : base(message, innerException)
    {
        Errors = [message];
    }

    public ApplicationValidationException(string[] errors) : base("Validation Failed")
    {
        Errors = errors;
    }

    public string[] Errors { get; private set; } = [];
}
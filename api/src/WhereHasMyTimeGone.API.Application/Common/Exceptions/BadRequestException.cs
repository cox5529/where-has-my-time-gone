namespace WhereHasMyTimeGone.API.Application.Common.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new List<string>();
    }

    public BadRequestException(IEnumerable<string> failures)
        : this()
    {
        Errors = failures;
    }

    public IEnumerable<string> Errors { get; }
}

using System.Net;

namespace Briefly.Core.Exceptions;

public class BrieflyException : Exception
{
    public BrieflyException(string message, IEnumerable<string> errors,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public BrieflyException(string message) : base(message)
    {
        ErrorMessages = new List<string>();
    }

    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }
}
using System.Net;

namespace ESOF.WebApp.WebAPI.Errors;

public class HttpErrorException : Exception
{
    public HttpStatusCode StatusCode { get; init; }
    public string Description { get; init; }

    public HttpErrorException(HttpStatusCode statusCode, string description)
    {
        StatusCode = statusCode;
        Description = description;
    }
}

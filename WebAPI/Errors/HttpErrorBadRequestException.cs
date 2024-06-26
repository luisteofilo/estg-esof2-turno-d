using System.Net;

namespace ESOF.WebApp.WebAPI.Errors;

public class HttpErrorBadRequestException : HttpErrorException
{
    public HttpErrorBadRequestException(string description) : base(HttpStatusCode.BadRequest, description) { }
}

namespace ESOF.WebApp.WebAPI.Errors;

public class NullUrlException : HttpErrorBadRequestException
{
    public NullUrlException() : base("The url is not valid!") { }
}

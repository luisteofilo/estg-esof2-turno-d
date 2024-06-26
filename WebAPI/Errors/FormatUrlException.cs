namespace ESOF.WebApp.WebAPI.Errors;

public class FormatUrlException : HttpErrorBadRequestException
{
    public FormatUrlException() : base("The url is not valid!") { }
}

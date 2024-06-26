using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

public abstract class BaseController : ControllerBase
{
    public BaseController() { }

    protected IActionResult HandleException(Exception ex)
    {
        return Problem(title: ex.Message, statusCode: 500);
    }

    protected IActionResult CreateResponse<T>(T data, string message = "", int statusCode = 200)
    {
        return StatusCode(statusCode, new
        {
            Message = message,
            Data = data
        });
    }
}


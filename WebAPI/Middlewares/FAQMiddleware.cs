using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Middlewares;

public class FAQMiddleware
{
    private readonly RequestDelegate _next;
    
    public FAQMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        var db = new ApplicationDbContext();
        var user = await db.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserId == Guid.Parse(context.User.Identity.Name));
        await _next(context);
    }
    
    private bool UserCanAnswer(User user)
    {
        return user.UserRoles.Any(ur => ur.Role.Name == "Admin");
    }
}
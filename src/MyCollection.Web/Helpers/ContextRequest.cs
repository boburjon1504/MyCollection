using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Enums;
using System.Security.Claims;

namespace MyCollection.Web.Helpers;

public class ContextRequest(IUserService userService,IHttpContextAccessor httpContext) : IContextRequest
{
    private readonly ClaimsPrincipal _principal = httpContext.HttpContext.User;
    public async ValueTask<User?> GetRequestedUserAsync()
    {
        if (!IsAuthorized())
            return new User();

        return await userService.GetByIdAsync(GetUserId());
    }
    
    public bool IsAuthorized() => _principal.Identity.IsAuthenticated;

    public Role GetRole() => Enum.Parse<Role>(_principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);

    public Guid GetUserId() => Guid.Parse(
        _principal.Claims.FirstOrDefault(c => c.Type == "UserId").Value
        );
}

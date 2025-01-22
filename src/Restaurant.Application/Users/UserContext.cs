using Microsoft.AspNetCore.Http;
using System.Data;
using System.Security.Claims;

namespace Restaurants.Application.Users
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var dateOfBirth = user.FindFirst(c=>c.Type == "DateOfBirth")?.Value;
            var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;

            var birthday = dateOfBirth == null ? (DateOnly?)null : DateOnly.ParseExact(dateOfBirth, "yyyy-MM-dd");

            return new CurrentUser(userId, email, roles,nationality, birthday);
        }
    }
}

using OegFlow.Domain;
using System.Security.Claims;

namespace OrgFlow.Api.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserContext userContext)
        {
            var user = context.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst("UserId")?.Value;
                var orgId = user.FindFirst("OrganizationId")?.Value;
                var deptId = user.FindFirst("DepartmentId")?.Value;
                var role = user.FindFirst(ClaimTypes.Role)?.Value;

                userContext.UserId = userId != null ? int.Parse(userId) : 0;
                userContext.OrganizationId = orgId != null ? int.Parse(orgId) : 0;
                userContext.DepartmentId = deptId != null ? int.Parse(deptId) : 0;
                userContext.Role = role;
            }

            await _next(context);
        }
    }

}

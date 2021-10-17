using BlogDALOld.Authorization;
using BlogDALOld.Policies;
using Microsoft.AspNetCore.Authorization;

namespace BlogDALOld.Models
{
    public static class Policies
    {
        public static AuthorizationPolicy ViewArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(RoleConstants.Member).Build();
        }
        public static AuthorizationPolicy CreateArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(RoleConstants.Member).Build();
        }
        public static AuthorizationPolicy EditArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(RoleConstants.Moderator).Build();
        }
        public static AuthorizationPolicy DeleteArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(RoleConstants.Admin).Build();
        }
    }
}

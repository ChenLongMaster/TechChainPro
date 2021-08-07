using BlogDAL.Policies;
using Microsoft.AspNetCore.Authorization;

namespace BlogDAL.Models
{
    public static class Policies
    {
        public static AuthorizationPolicy ViewArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(PolicyConstants.Member).Build();
        }
        public static AuthorizationPolicy CreateArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(PolicyConstants.Member).Build();
        }
        public static AuthorizationPolicy EditArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(PolicyConstants.Moderator).Build();
        }
        public static AuthorizationPolicy DeleteArticlePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(PolicyConstants.Admin).Build();
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Policies
{
    public static class PolicyConstants
    {
        public const string Admin = "Admin";
        public const string Moderator = "Moderator";
        public const string Member = "Member";

        public const string ViewArticle = "ViewArticlePolicy";
        public const string CreateArticle = "CreateArticlePolicy";
        public const string EditArticle = "EditArticlePolicy";
        public const string DeleteArticle = "DeleteArticlePolicy";
    }
}

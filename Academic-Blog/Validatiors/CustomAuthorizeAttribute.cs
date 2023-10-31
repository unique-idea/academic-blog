using Academic_Blog.Enums;
using Academic_Blog.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Academic_Blog.Validatiors
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params RoleEnum[] roleEnums)
        {
            var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}

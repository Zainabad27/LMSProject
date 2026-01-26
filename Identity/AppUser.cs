using Microsoft.AspNetCore.Identity;

namespace LmsApp2.Api.Identity
{
    public class AppUser : IdentityUser
    {
        public Guid UserId_InMainTable { get; set; }
    }
}
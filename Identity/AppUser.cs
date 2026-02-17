using LmsApp2.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace LmsApp2.Api.Identity
{
    public class AppUser : IdentityUser
    {
        public Guid UserId_InMainTable { get; set; }


        public string? RefreshToken { get; set; }

        public DateTime TokenExpiry { get; set; }





        public virtual Employee? Employee { get; set; }
    }
}
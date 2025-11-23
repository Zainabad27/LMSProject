using System.ComponentModel.DataAnnotations;

namespace LmsApp2.Api.DTOs
{
    public class RefreshAccessTokenDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}

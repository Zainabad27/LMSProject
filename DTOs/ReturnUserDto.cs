namespace LmsApp2.Api.DTOs
{
    public class ReturnUserDto(string role, string id)
    {
        public string Role { get; set; } = role;
        public string UserId { get; set; } = id;
    }

}
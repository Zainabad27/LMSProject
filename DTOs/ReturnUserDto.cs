namespace LmsApp2.Api.DTOs
{
    public class ReturnUserDto(string role)
    {
        public string Role { get; set; } = role;
    }

}
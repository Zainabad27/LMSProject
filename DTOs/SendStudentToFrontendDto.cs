
namespace LmsApp2.Api.DTOs
{
    public class SendStudentsToFrontendDto
    {
        public string? StudentName { get; set; } 
        public Guid StudentId { get; set; }

        public string? Gender { get; set; } 

        public bool IsActive { get; set; }

        // public string? Photo { get; set; }       

        // public string? Email { get; set; }  

        // public string? PhoneNumber { get; set; }    

        // public string? Address { get; set; }

    }
}



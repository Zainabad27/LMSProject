namespace LmsApp2.Api.DTOs
{
    public class SendLoginDataToFrontend
    {
        public string RefreshToken { get; set; }=string.Empty;  

        public string AccessToken { get; set; }=string.Empty;


        public Guid UserId_InMainTable { get; set; }


    }
}




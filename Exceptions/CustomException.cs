namespace LmsApp2.Api.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
 
        public CustomException(string message ="Something went wrong.", int statusCode=400) : base(message)
        {

            StatusCode = statusCode;

        }
    }
}

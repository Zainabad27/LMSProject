using LmsApp2.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp2.Api.Utilities
{
    public class FetchFileFromServer
    {
        IWebHostEnvironment env;
        public FetchFileFromServer(IWebHostEnvironment env)
        {
            this.env = env;

        }
        public byte[] FetchFile(string Path)
        {
            string FullPath = env!.WebRootPath + "/" + Path;

            if (!System.IO.File.Exists(Path))
            {
                throw new CustomException("Some internal server error occured, File not found", 500);
            }




            byte[] Data = File.ReadAllBytes(Path);


            return Data;




        }

    }
}

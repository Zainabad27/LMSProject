namespace LmsApp2.Api.Utilities
{
    public static class FileUploadToServer
    {

        public static async Task<string> UploadToServer(this IFormFile file, string DirectoryPath)
        {




            string FilePath = Path.Combine(DirectoryPath, file.FileName);


            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return FilePath;

        }





    }
}

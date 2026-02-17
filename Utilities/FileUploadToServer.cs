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




        public static Task DeleteFileFromServer(this Dictionary<string, string> docs)
        {
            foreach (var (docName, path) in docs)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        Console.WriteLine($"Deleted: {docName} -> {path}");
                    }
                    else
                    {
                        Console.WriteLine($"File not found: {docName} -> {path}");
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"File in use: {path} | {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"No permission to delete: {path} | {ex.Message}");
                }
            }

            return Task.CompletedTask;
        }


    }
}

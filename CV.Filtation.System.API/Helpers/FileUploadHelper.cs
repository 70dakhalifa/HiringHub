namespace CV.Filtation.System.API.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> SaveUploadedFileAsync(IFormFile file, string uploadFolder)
        {
            Directory.CreateDirectory(uploadFolder);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CodeYad_Blog.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public string SaveFile(IFormFile file, string SavePath)
        {
            if (file == null)
                throw new Exception("File Is Null");
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), SavePath.Replace("/","\\"));

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }


            var fullPath = Path.Combine(FolderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);


            file.CopyTo(stream);
                return fileName;
        }
    }
}
